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
        //private GoalHandler m_GoalHandler;
        private readonly IGridView _gridView;

        public LevelPresenter(LevelStateHandler levelStateHandler, IGridView gridView)
        {
            _levelStateHandler = levelStateHandler;
            _gridView = gridView;
            GameEventSystem.AddListener<OnLevelLoadedEvent>(LevelLoaded);
            //_gridView = DI.Resolve<IGridView>();
        }

        ~LevelPresenter()
        {
            GameEventSystem.RemoveListener<OnLevelLoadedEvent>(LevelLoaded);
            //m_SignalBus.Unsubscribe<OnLevelRequestedSignal>(LevelRequested);
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
            Debug.Log(levelInfo);
            _gridView.CalculateGridSize(levelInfo.GridSize);
            _levelStateHandler.Initialize(levelInfo);
            //m_GoalHandler.Initialize(levelInfo);
            _gridView.ScaleGrid();
        }
    }
}
