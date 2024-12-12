using DI;
using Events;
using Events.Level;
using MVP.Presenters.Handlers;
using MVP.Views.Interface;
using UnityEngine;

namespace MVP.Presenters
{
    public class LevelPresenter
    {
        private readonly LevelStateHandler _levelStateHandler;
        private readonly GoalHandler _goalHandler;
        private readonly IGridView _gridView;

        public LevelPresenter(LevelStateHandler levelStateHandler, GoalHandler goalHandler, IGridView gridView)
        {
            _levelStateHandler = levelStateHandler;
            _goalHandler = goalHandler;
            _gridView = gridView;
            GameEventSystem.AddListener<OnLevelLoadedEvent>(LevelLoaded);
        }

        ~LevelPresenter()
        {
            GameEventSystem.RemoveListener<OnLevelLoadedEvent>(LevelLoaded);
            //m_GoalHandler.OnLevelCompleted -= HandleLevelCompleted;
            //m_GoalHandler.OnLevelFailed -= HandleLevelFailed;
        }

        private void HandleLevelCompleted()
        {
            //m_LevelStateHandler.CompleteLevel();
        }

        private void HandleLevelFailed()
        {
            //m_LevelStateHandler.FailLevel();
        }

        //After the button clicked for retry or next level
        private void LevelRequested()
        {
            //m_LevelStateHandler.RequestLevel();
        }

        private void LevelLoaded(object args)
        {
            Debug.Log("LevelLoaded");
            var levelInfo = (args as OnLevelLoadedEvent)?.LevelInfo;
            _gridView.CalculateGridSize(levelInfo.GridSize);
            _levelStateHandler.Initialize(levelInfo);
            _goalHandler.Initialize(levelInfo.Goals, levelInfo.NumberOfMoves);
            _gridView.ScaleGrid();
        }
    }
}
