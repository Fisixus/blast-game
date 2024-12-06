using System;
using System.Collections.Generic;
using Core.Enum;

namespace LevelBase
{
    public class LevelInfo
    {
        public ItemType[,] GridItems;
        public List<LevelGoal> Goals;
        public int NumberOfMoves;
        //public ItemSpawnProbabilityDistributionSO Probability;

        public LevelInfo(ItemType[,] gridItems, List<LevelGoal> goals, int numberOfMoves)
        {
            GridItems = gridItems;
            Goals = goals;
            NumberOfMoves = numberOfMoves;
        }
    }
}
