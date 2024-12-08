using Core.GridElements.GridPawns;
using Core.GridElements.GridPawns.Effect;
using Events;
using Events.Grid;
using MVP.Models.Interface;
using MVP.Presenters.Handlers;
using MVP.Views.Interface;
using UnityEngine;

namespace MVP.Presenters
{
    public class MatchPresenter
    {
        private readonly IGridView _gridView;
        private readonly IGridModel _gridModel;
        private readonly MatchHandler _matchHandler;


        public MatchPresenter(IGridView gridView, IGridModel gridModel, MatchHandler matchHandler)
        {
            _gridView = gridView;
            _gridModel = gridModel;
            _matchHandler = matchHandler;
            
            GameEventSystem.AddListener<OnGridObjectTouchedEvent>(OnTouch);
            GameEventSystem.AddListener<OnGridObjectInitializedEvent>(GridObjectInitializedInGrid);
            GameEventSystem.AddListener<OnGridObjectShiftedEvent>(GridObjectShiftedInGrid);
            GameEventSystem.AddListener<OnGridObjectUpdatedEvent>(GridObjectUpdatedInGrid);
        }
        
        ~MatchPresenter()
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
                    //ProcessBoosterTouchAsync(booster).Forget();
                    break;
            }
        }
        
        private void ProcessItemTouch(Item item)
        {
            var matchedItems = _matchHandler.FindMatches(item);
            if (matchedItems.Count == 0)
            {
                ProcessNoMatch(item);
                return;
            }

            var (obstacles, regularItems) = GridItemFinderHelper.SeparateRegularItems(matchedItems);
            // var boosterType = m_BoosterHandler.IsBoosterCreatable(nonBalloons);
            // if (boosterType != BoosterType.None)
            // {
            //     ProcessBoosterCreation(item, balloons, nonBalloons, boosterType);
            // }
            // else
            // {
            //     m_GoalHandler.UpdateMoves();
            //     ProcessMatch(matchedItems);
            // }
        }
        
        private void ProcessNoMatch(BaseGridObject touchedGridObject)
        {
            touchedGridObject.GetComponent<BaseGridObjectEffect>().Shake();
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
            _gridView.SetGridObjectLocation(eventArgs.GridObject, newCoord:new Vector2Int(), isAnimOn: false); //TODO:
            _gridView.SetGridObjectLocation(eventArgs.GridObject, isAnimOn: eventArgs.IsAnimationOn, animationTime: 0.6f);
        }
    }
}
