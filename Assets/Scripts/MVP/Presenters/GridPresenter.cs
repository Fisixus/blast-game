using System;
using System.Collections.Generic;
using System.Linq;
using Core.GridElements.Enums;
using Core.GridElements.GridPawns;
using Core.GridElements.GridPawns.Effect;
using Cysharp.Threading.Tasks;
using Events;
using Events.Grid;
using Events.Input;
using MVP.Models.Interface;
using MVP.Presenters.Handlers;
using MVP.Presenters.Handlers.Effects;
using MVP.Views.Interface;
using UnityEngine;
using OnGridObjectTouchedEvent = Events.Grid.OnGridObjectTouchedEvent;

namespace MVP.Presenters
{
    public class GridPresenter
    {
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
            
            GameEventSystem.AddListener<OnGridObjectTouchedEvent>(OnTouch);
            GameEventSystem.AddListener<OnGridObjectInitializedEvent>(GridObjectInitializedInGrid);
            GameEventSystem.AddListener<OnGridObjectShiftedEvent>(GridObjectShiftedInGrid);
            GameEventSystem.AddListener<OnGridObjectUpdatedEvent>(GridObjectUpdatedInGrid);
        }
        
        ~GridPresenter()
        {
            GameEventSystem.RemoveListener<OnGridObjectTouchedEvent>(OnTouch);
            GameEventSystem.RemoveListener<OnGridObjectInitializedEvent>(GridObjectInitializedInGrid);
            GameEventSystem.RemoveListener<OnGridObjectShiftedEvent>(GridObjectShiftedInGrid);
            GameEventSystem.RemoveListener<OnGridObjectUpdatedEvent>(GridObjectUpdatedInGrid);
            //m_GoalHandler.OnLevelCompleted -= HandleLevelCompleted;
            //m_GoalHandler.OnLevelFailed -= HandleLevelFailed;
        }
        
        private void OnTouch(object args)
        {
            var eventArgs = (OnGridObjectTouchedEvent)args;
            switch (eventArgs.GridObject)
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
            var matchedGridObjects = _hintHandler.GetSelectedMatchedItems(item).ToList();
            if (matchedGridObjects.Count == 0)
            {
                ProcessNoMatch(item);
                return;
            }
            var boosterType = item.HintType;
            var effectedObstacles = _matchHandler.FindObstacles(matchedGridObjects);
            if (boosterType != BoosterType.None)
            {
                await ProcessBoosterCreationAsync(item, matchedGridObjects, effectedObstacles, boosterType);
            }
            else
            {
                matchedGridObjects.AddRange(effectedObstacles);
                _blastEffectHandler.PlayBlastParticles(matchedGridObjects); //TODO:
                ProcessMatch(matchedGridObjects);
            }
        }
        
        private async UniTask ProcessBoosterCreationAsync(Item item, List<BaseGridObject> matchedObjects, List<BaseGridObject> effectedObstacles, BoosterType boosterType)
        {
            effectedObstacles.ForEach(obstacle => obstacle.gameObject.SetActive(false));//TODO:
            _blastEffectHandler.PlayBlastParticles(effectedObstacles); //TODO:

            // Update input state
            GameEventSystem.Invoke<OnInputStateChangedEvent>(new OnInputStateChangedEvent() { IsInputOn = false });

            // Animate booster creation
            await _boosterHandler.AnimateBoosterCreationAsync(item, matchedObjects);

            // Create booster and update grid model
            var booster = _gridObjectFactoryHandler.CreateBoosterAndDestroyOldItem(item, boosterType);
            _gridModel.UpdateGridObjects(new List<BaseGridObject> { booster }, false);

            // Update moves and process matches
            matchedObjects.AddRange(effectedObstacles);
            ProcessMatch(matchedObjects, true);

            // Re-enable input
            GameEventSystem.Invoke<OnInputStateChangedEvent>(new OnInputStateChangedEvent() { IsInputOn = true });
        }
        private async UniTaskVoid ProcessBoosterTouchAsync(Booster booster)
        {
            // Find all matching boosters for the touched booster
            var boosters = _matchHandler.FindBoosterMatches(booster).Cast<Booster>().ToList();
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
            // Disable input during combo handling
            GameEventSystem.Invoke<OnInputStateChangedEvent>(new OnInputStateChangedEvent() { IsInputOn = false });

            // Animate combo creation and wait for completion
            await _comboHandler.AnimateComboCreationAsync(centerBooster, boosters);

            // Merge boosters into a combo
            var comboType = _comboHandler.MergeBoosters(boosters);
            var combo = _gridObjectFactoryHandler.CreateComboAndDestroyOldBooster(centerBooster, comboType);
            _gridModel.UpdateGridObjects(new List<BaseGridObject> { combo }, false);

            // Update moves and process matches
            //ProcessMatch(boosters, false);

            // Re-enable input
            GameEventSystem.Invoke<OnInputStateChangedEvent>(new OnInputStateChangedEvent() { IsInputOn = true });

            // Apply the combo effect and get affected grid objects
            
            //await UniTask.Delay(TimeSpan.FromSeconds(0.25f), DelayType.DeltaTime);//TODO:
            
            var effectedGridObjects = await _boosterHandler.ApplyBoostAsync(combo);

            // Process matches after applying the combo
            effectedGridObjects.AddRange(boosters);
            ProcessMatch(effectedGridObjects, true);
        }
        
        private async UniTask HandleSingleBoostAsync(Booster booster)
        {
            // Apply the single booster effect and get affected grid objects
            var effectedGridObjects = await _boosterHandler.ApplyBoostAsync(booster);

            // Process matches after applying the booster
            ProcessMatch(effectedGridObjects, true);
        }

        
        private void ProcessNoMatch(BaseGridObject touchedGridObject)
        {
            touchedGridObject.GetComponent<BaseGridObjectEffect>().Shake();
        }
        
        private void ProcessMatch(IEnumerable<BaseGridObject> matchedObjs, bool shouldUpdateGoals = true)
        {
            var baseGridObjects = matchedObjs.Distinct().ToList();//TODO:Don't use distinct find the same items
            if (shouldUpdateGoals)
            {
                var obstaclesOnly = baseGridObjects.OfType<Obstacle>().ToList();
                _goalHandler.UpdateGoals(obstaclesOnly);
            }
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
        
        private void GridObjectInitializedInGrid(object args)
        {
            var eventArgs = (OnGridObjectInitializedEvent)args;
            _gridView.SetGridObjectLocation(eventArgs.GridObject);
        }

        private void GridObjectShiftedInGrid(object args)
        {
            var eventArgs = (OnGridObjectShiftedEvent)args;
            _gridView.SetGridObjectLocation(eventArgs.GridObject, isAnimOn: true, animationTime: 0.5f);
        }

        private void GridObjectUpdatedInGrid(object args)
        {
            var eventArgs = (OnGridObjectUpdatedEvent)args;
            var newItem = eventArgs.GridObject;
            _gridView.SetGridObjectLocation(newItem, newCoord:new Vector2Int(newItem.Coordinate.x, newItem.Coordinate.y - _gridModel.ColumnCount), isAnimOn: false);
            _gridView.SetGridObjectLocation(newItem, isAnimOn: eventArgs.IsAnimationOn, animationTime: 0.6f);
        }
    }
}
