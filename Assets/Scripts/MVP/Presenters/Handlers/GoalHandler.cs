using System;
using System.Collections.Generic;
using System.Linq;
using Core.GridElements.Enums;
using Core.GridElements.GridPawns;
using Core.GridElements.UI;
using UnityEngine;

namespace MVP.Presenters.Handlers
{
    public class GoalHandler
    {
        public event Action OnLevelCompleted;
        public event Action OnLevelFailed;

        private readonly GoalUIHandler _goalUIHandler;


        private LevelGoal[] _goals;
        private int _numberOfMoves;
        private bool _levelCompleted;

        public GoalHandler(GoalUIHandler goalUIHandler)
        {
            _goalUIHandler = goalUIHandler;
        }

        public void Initialize(List<LevelGoal> levelGoals, int levelNumberOfMoves)
        {
            // Clone the provided goals into the internal goals array
            _goals = levelGoals.Select(goal => goal.Clone()).ToArray();
            _numberOfMoves = levelNumberOfMoves;
            _goalUIHandler.Initialize(_goals, _numberOfMoves);

            _levelCompleted = false;
        }

        public void UpdateMoves()
        {
            DecreaseMoveCounter();
            CheckLevelEndConditions();
        }
        
        public void UpdateGoals(List<Obstacle> matchedObstacles)
        {
            UpdateGoalCounts(matchedObstacles);
            CheckLevelEndConditions();
        }

        // Helper methods to reduce redundancy
        private void DecreaseMoveCounter()
        {
            _goalUIHandler.UpdateMoveCounter(Mathf.Max(--_numberOfMoves, 0));
        }

        // Updates goal counts based on matched obstacles
        private void UpdateGoalCounts(List<Obstacle> matchedObstacles)
        {
            // Group matched obstacles by Type
            var obstaclesByType = matchedObstacles
                .GroupBy(obstacle => obstacle.ObstacleType)
                .ToDictionary(group => group.Key, group => group.ToList());

            // Filter and group matched obstacles by goals
            var groupedObstaclesByGoal = _goals
                .Where(goal => obstaclesByType.ContainsKey(goal.ObstacleType))
                .ToDictionary(goal => goal, goal => obstaclesByType[goal.ObstacleType]);

            // Process each goal and its corresponding matched items
            foreach (var (goal, matchedObstaclesForGoal) in groupedObstaclesByGoal)
            {
                if (goal.Count <= 0) continue;
                goal.Count = Mathf.Max(goal.Count - matchedObstaclesForGoal.Count, 0);
                _goalUIHandler.UpdateGoalUI(goal);
            }
        }
        
        // Checks if goals or moves have completed and triggers end-of-level UI
        private void CheckLevelEndConditions()
        {
            if (_levelCompleted) return;
            if (AreAllGoalsCompleted())
            {
                OnLevelCompleted?.Invoke();
                _levelCompleted = true;
            }
            else if (AreAllMovesFinished())
            {
                OnLevelFailed?.Invoke();
                _levelCompleted = true;
            }
        }

        private bool AreAllMovesFinished()
        {
            return _numberOfMoves == 0;
        }

        private bool AreAllGoalsCompleted()
        {
            return _goals.All(goal => goal.Count <= 0);
        }
    }
}