using System;
using Core.GridElements.Enums;
using UnityEngine;

namespace Core.GridElements.UI
{
    /// <summary>
    /// The LevelGoal class represents a goal for a specific level in the game. 
    /// Each goal is defined by an ItemType and a Count, representing the type of item needed to complete the goal and the number of those items respectively.
    /// </summary>
    [Serializable]
    public class LevelGoal
    {
        [field: SerializeField] public int Count { get; set; }
        [field: SerializeField] public ObstacleType ObstacleType { get; set; }

        public Vector3 WorldPos { get; set; }
        public event EventHandler<int> OnUIGoalUpdated;


        // Method to invoke the event
        public void RaiseUIGoalUpdated()
        {
            OnUIGoalUpdated?.Invoke(this, Count);
        }

        // Clone method
        public LevelGoal Clone()
        {
            return new LevelGoal
            {
                ObstacleType = ObstacleType,
                Count = Count
            };
        }
    }
}
