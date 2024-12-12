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
        private readonly MatchHandler _matchHandler;
        private readonly BoosterHandler _boosterHandler;
        private readonly ComboHandler _comboHandler;
        private readonly HintHandler _hintHandler;
        private readonly BlastEffectHandler _blastEffectHandler;
        private readonly GridShiftHandler _gridShiftHandler;
        private readonly GridObjectFactoryHandler _gridObjectFactoryHandler;
        


        public GridPresenter(IGridView gridView, IGridModel gridModel, MatchHandler matchHandler, BoosterHandler boosterHandler, ComboHandler comboHandler, 
            HintHandler hintHandler, BlastEffectHandler blastEffectHandler, GridShiftHandler gridShiftHandler, GridObjectFactoryHandler gridObjectFactoryHandler)
        {
            _gridView = gridView;
            _gridModel = gridModel;
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
        
        private void ProcessItemTouch(Item item)
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
                ProcessBoosterCreation(item, matchedGridObjects, effectedObstacles, boosterType);
            }
            else
            {
                // m_GoalHandler.UpdateMoves();
                matchedGridObjects.AddRange(effectedObstacles);
                _blastEffectHandler.PlayBlastParticles(matchedGridObjects); //TODO:
                ProcessMatch(matchedGridObjects);
            }
        }
        
        private void ProcessBoosterCreation(Item item, List<BaseGridObject> matchedObjects, List<BaseGridObject> effectedObstacles, BoosterType boosterType)
        {
            effectedObstacles.ForEach(obstacle => obstacle.gameObject.SetActive(false));//TODO:
            _blastEffectHandler.PlayBlastParticles(effectedObstacles);//TODO:
            //TODO: m_GoalHandler.UpdateGoals(effectedObstacles);
            GameEventSystem.Invoke<OnInputStateChangedEvent>(new OnInputStateChangedEvent(){IsInputOn = false});
        
            _boosterHandler.AnimateBoosterCreation(
                item,
                matchedObjects,
                (centerItem) =>
                {
                    var booster = _gridObjectFactoryHandler.CreateBoosterAndDestroyOldItem(centerItem, boosterType);
                    _gridModel.UpdateGridObjects(new List<BaseGridObject> { booster }, false);

                    //m_GoalHandler.UpdateMoves();
                    matchedObjects.AddRange(effectedObstacles);
                    ProcessMatch(matchedObjects, false);
                    ProcessMatch(effectedObstacles, true);
                    GameEventSystem.Invoke<OnInputStateChangedEvent>(new OnInputStateChangedEvent(){IsInputOn = true});
                });
        }
        
        private async UniTaskVoid ProcessBoosterTouchAsync(Booster booster)
        {
            var boosters = _matchHandler.FindBoosterMatches(booster).Cast<Booster>().ToList();
            var combo = _comboHandler.MergeBoosters(boosters, booster.Coordinate);
            //finalBooster.BoosterType = BoosterType.BombBomb;
            Debug.Log(combo);
            //ProcessMatch(boosters, false);

            var effectedGridObjects = await _boosterHandler.ApplyBoostAsync(combo);
            // Update moves after applying the boost
            //TODO:m_GoalHandler.UpdateMoves();

            // Process matches after boosting is complete
            ProcessMatch(effectedGridObjects, false);
        }

        
        private void ProcessNoMatch(BaseGridObject touchedGridObject)
        {
            touchedGridObject.GetComponent<BaseGridObjectEffect>().Shake();
        }
        
        private void ProcessMatch(IEnumerable<BaseGridObject> matchedObjs, bool shouldUpdateGoals = true)
        {
            var baseGridObjects = matchedObjs.ToList();
            // if (shouldUpdateGoals)
            // {
            //     var itemsOnly = baseGridObjects.OfType<Item>().ToList();
            //     m_GoalHandler.UpdateGoals(itemsOnly);
            // }
            ShiftAndReplaceGridObjects(baseGridObjects);
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
