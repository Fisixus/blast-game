using System;
using Core.Enum;
using UnityEngine;

namespace Core.LevelSerialization
{
    /// <summary>
    /// The LevelGoal class represents a goal for a specific level in the game. 
    /// Each goal is defined by an ItemType and a Count, representing the type of item needed to complete the goal and the number of those items respectively.
    /// </summary>
    [Serializable]
    public class LevelGoal
    {
        [field: SerializeField] public int Count { get; set; }
        [field: SerializeField] public ItemType ItemType { get; set; }

        [NonSerialized] public Vector3 WorldPos;
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
                ItemType = ItemType,
                Count = Count
            };
        }
    }
}
