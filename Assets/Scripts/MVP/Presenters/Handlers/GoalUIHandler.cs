using System.Collections.Generic;
using Core.Factories.Interface;
using Core.GridElements.UI;
using MVP.Views.Interface;
using UTasks;

namespace MVP.Presenters.Handlers
{
    public class GoalUIHandler
    {
        private readonly ILevelUIView _levelUIView;
        private readonly IGoalUIFactory _goalUIFactory;

        private List<GoalUI> _goalUIs;

        public GoalUIHandler(ILevelUIView uiView, IGoalUIFactory goalUIFactory)
        {
            _levelUIView = uiView;
            _goalUIFactory = goalUIFactory;
        }

        public void Initialize(LevelGoal[] goals, int numberOfMoves)
        {
            DestroyGoalUIs();
            CreateGoalUIs(goals);
            UpdateWorldPositions(goals, _goalUIs);
            _levelUIView.SetMoveCounter(numberOfMoves);
        }

        private void DestroyGoalUIs()
        {
            if (_goalUIs == null || _goalUIs.Count == 0) return;

            _goalUIFactory.DestroyObjs(_goalUIs);
            _goalUIs.Clear();
        }

        private void CreateGoalUIs(LevelGoal[] goals)
        {
            _goalUIs = new List<GoalUI>(goals.Length);
            for (var i = 0; i < goals.Length; i++)
            {
                var tagGoalUI = _goalUIFactory.CreateObj();
                _goalUIs.Add(tagGoalUI);
                tagGoalUI.gameObject.name = $"Goal:{goals[i].ObstacleType}";
                var goalUIData = _goalUIFactory.GoalUIDataDict[goals[i].ObstacleType];
                _levelUIView.InitializeGoal(goalUIData.ObstacleTexture, goalUIData.ObstacleWidthHeight, tagGoalUI,
                    goals[i]);
            }
        }

        private void UpdateWorldPositions(LevelGoal[] goals, List<GoalUI> tagGoalUIs)
        {
            UTask.WaitFrames(5).Do(() =>
            {
                for (var i = 0; i < tagGoalUIs.Count; i++) goals[i].WorldPos = tagGoalUIs[i].transform.position;
            });
        }

        public void UpdateGoalUI(LevelGoal goal)
        {
            goal.RaiseUIGoalUpdated();
        }

        public void UpdateMoveCounter(int totalMoveCount)
        {
            _levelUIView.SetMoveCounter(totalMoveCount);
        }
    }
}