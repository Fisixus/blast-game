using System.Collections.Generic;
using System.Linq;
using Core.GridElements.Enums;
using Core.GridElements.GridPawns;
using Cysharp.Threading.Tasks;
using Input;
using MVP.Models.Interface;
using MVP.Presenters.Handlers;
using MVP.Presenters.Handlers.Effects;
using MVP.Views.Interface;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MVP.Presenters
{
    public class GridPresenter
    {
        //private readonly UserInput _userInput;
        private readonly IGridView _gridView;
        private readonly IGridModel _gridModel;
        private readonly GoalHandler _goalHandler;
        private readonly MatchHandler _matchHandler;
        private readonly BoosterHandler _boosterHandler;
        private readonly ComboHandler _comboHandler;
        private readonly HintHandler _hintHandler;
        private readonly BlastEffectHandler _blastEffectHandler;
        private readonly GridShiftHandler _gridShiftHandler;
        private readonly GridObjectFactoryHandler _gridObjectFactoryHandler;

        public GridPresenter(IGridView gridView, IGridModel gridModel, GoalHandler goalHandler, MatchHandler matchHandler, BoosterHandler boosterHandler, ComboHandler comboHandler, 
            HintHandler hintHandler, BlastEffectHandler blastEffectHandler, GridShiftHandler gridShiftHandler, GridObjectFactoryHandler gridObjectFactoryHandler)
        {
            _gridView = gridView;
            _gridModel = gridModel;
            _goalHandler = goalHandler;
            _matchHandler = matchHandler;
            _boosterHandler = boosterHandler;
            _comboHandler = comboHandler;
            _hintHandler = hintHandler;
            _blastEffectHandler = blastEffectHandler;
            _gridShiftHandler = gridShiftHandler;
            _gridObjectFactoryHandler = gridObjectFactoryHandler;
            
            UserInput.OnGridObjectTouched += OnTouch;
            _gridModel.OnGridObjectInitializedEvent += GridObjectInitializedInGrid;
            _gridShiftHandler.OnGridObjectShiftedEvent += GridObjectShiftedInGrid;
            _gridModel.OnGridObjectUpdatedEvent += GridObjectUpdatedInGrid;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }
        private void OnSceneUnloaded(Scene scene)
        {
            Dispose();
        }
        private void Dispose()
        {
            // Unsubscribe from static and instance events
            UserInput.OnGridObjectTouched -= OnTouch;
            _gridModel.OnGridObjectInitializedEvent -= GridObjectInitializedInGrid;
            _gridShiftHandler.OnGridObjectShiftedEvent -= GridObjectShiftedInGrid;
            _gridModel.OnGridObjectUpdatedEvent -= GridObjectUpdatedInGrid;

            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }
        private void OnTouch(BaseGridObject touchedGridObject)
        {
            switch (touchedGridObject)
            {
                case Item item:
                    ProcessItemTouch(item);
                    break;
                case Booster booster:
                    ProcessBoosterTouchAsync(booster).Forget();
                    break;
                case Obstacle obstacle:
                    ProcessNoMatch(obstacle);
                    break;
            }
        }
        
        private async void ProcessItemTouch(Item item)
        {
            var matchedGridObjects = _hintHandler.GetSelectedMatchedGridObjects(item).ToList();
            if (matchedGridObjects.Count == 0)
            {
                ProcessNoMatch(item);
                return;
            }
            var boosterType = item.HintType;
            var effectedObstacles = _matchHandler.FindObstacles(matchedGridObjects);
            effectedObstacles.ForEach(obstacle => obstacle.IsEmpty = true);
            _blastEffectHandler.PlayBlastParticles(effectedObstacles);
            
            if (boosterType != BoosterType.None)
            {
                await ProcessBoosterCreationAsync(item, matchedGridObjects, effectedObstacles, boosterType);
            }
            else
            {
                matchedGridObjects.AddRange(effectedObstacles);
                _blastEffectHandler.PlayBlastParticles(matchedGridObjects);
                ProcessMatch(matchedGridObjects);
            }
        }
        
        private async UniTask ProcessBoosterCreationAsync(Item item, List<BaseGridObject> matchedObjects, List<BaseGridObject> effectedObstacles, BoosterType boosterType)
        {
            // Update input state
            UserInput.SetInputState(false);

            // Animate booster creation
            await _boosterHandler.AnimateBoosterCreationAsync(item, matchedObjects);

            // Create booster and update grid model
            var booster = _gridObjectFactoryHandler.CreateBoosterAndDestroyOldItem(item, boosterType);
            _gridModel.UpdateGridObjects(new List<BaseGridObject> { booster }, false);

            // Update moves and process matches
            matchedObjects.AddRange(effectedObstacles);
            ProcessMatch(matchedObjects);

            // Re-enable input
            UserInput.SetInputState(true);
        }
        private async UniTaskVoid ProcessBoosterTouchAsync(Booster booster)
        {
            // Find all matching boosters for the touched booster
            var boosters = _hintHandler.GetSelectedMatchedGridObjects(booster).Cast<Booster>().ToList();
            // Determine if a combo should be created or a single booster applied
            if (boosters.Count > 1)
            {
                await HandleComboBoostAsync(boosters, booster);
            }
            else
            {
                await HandleSingleBoostAsync(booster);
            }
        }

        private async UniTask HandleComboBoostAsync(List<Booster> boosters, Booster centerBooster)
        {
            UserInput.SetInputState(false);

            // Animate combo creation and wait for completion
            await _comboHandler.AnimateComboCreationAsync(centerBooster, boosters);

            // Merge boosters into a combo
            var comboType = _comboHandler.MergeBoosters(boosters);
            var combo = _gridObjectFactoryHandler.CreateComboAndDestroyOldBooster(centerBooster, comboType);
            _gridModel.UpdateGridObjects(new List<BaseGridObject> { combo }, false);
            //ProcessMatch(boosters, false);


            // Apply the combo effect and get affected grid objects
            //await UniTask.Delay(TimeSpan.FromSeconds(0.15f), DelayType.DeltaTime);//TODO:Play combo animation if it exists
            UserInput.SetInputState(true);
            //_userInput.SetInputState(true);
            
            var effectedGridObjects = await _boosterHandler.ApplyBoostAsync(combo);

            // Process matches after applying the combo
            _gridObjectFactoryHandler.DestroyCombo(combo);
            effectedGridObjects.AddRange(boosters);
            ProcessMatch(effectedGridObjects);
        }
        
        private async UniTask HandleSingleBoostAsync(Booster booster)
        {
            // Apply the single booster effect and get affected grid objects
            var effectedGridObjects = await _boosterHandler.ApplyBoostAsync(booster);

            // Process matches after applying the booster
            ProcessMatch(effectedGridObjects);
        }

        
        private void ProcessNoMatch(BaseGridObject touchedGridObject)
        {
            touchedGridObject.BaseGridObjectEffect.Shake();
        }
        
        private void ProcessMatch(IEnumerable<BaseGridObject> matchedObjs)
        {
            var baseGridObjects = matchedObjs.Distinct().ToList();
            
            var obstaclesOnly = baseGridObjects.OfType<Obstacle>().ToList();
            _goalHandler.UpdateGoals(obstaclesOnly);
            
            ShiftAndReplaceGridObjects(baseGridObjects);
            
            _goalHandler.UpdateMoves();
            _hintHandler.DetectAndSetHints();
            baseGridObjects.Clear();
        }
        
        private void ShiftAndReplaceGridObjects(List<BaseGridObject> matchedGridObjects)
        {
            // Perform shifting and replacement
            var newItems = _gridShiftHandler.ShiftAndReplace(
                _gridModel.Grid,
                _gridModel.ColumnCount,
                _gridModel.RowCount,
                matchedGridObjects
            );
            
            // Update the grid with new items
            _gridModel.UpdateGridObjects(newItems, true);
        }
        
        private void GridObjectInitializedInGrid(BaseGridObject obj)
        {
            obj.SetWorldPosition(_gridView.CellSize, _gridView.GridTopLeftTr, null, false);
        }

        private void GridObjectShiftedInGrid(BaseGridObject obj)
        {
            obj.SetWorldPosition(_gridView.CellSize, _gridView.GridTopLeftTr, null, true, 0.5f);
        }

        private void GridObjectUpdatedInGrid(BaseGridObject obj, bool isAnimOn)
        {
            obj.SetWorldPosition(_gridView.CellSize, _gridView.GridTopLeftTr,new Vector2Int(obj.Coordinate.x, obj.Coordinate.y - _gridModel.ColumnCount), false);
            obj.SetWorldPosition(_gridView.CellSize, _gridView.GridTopLeftTr, null, isAnimOn, 0.6f);
        }
    }
}
