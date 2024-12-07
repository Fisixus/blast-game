using Events;
using Events.Level;
using MVP.Views.Interface;

namespace MVP.Presenters
{
    public class LevelPresenter
    {
        //private LevelStateHandler m_LevelStateHandler;
        //private GoalHandler m_GoalHandler;
        private IGridView _gridView;

        public LevelPresenter()
        {
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
            var levelInfo = (args as OnLevelLoadedEvent)?.LevelInfo;
            _gridView.CalculateGridSize(levelInfo.GridSize);
            //m_LevelStateHandler.Initialize(levelInfo);
            //m_GoalHandler.Initialize(levelInfo);
            _gridView.ScaleGrid();
        }
    }
}
