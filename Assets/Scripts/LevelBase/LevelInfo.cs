using System;
using System.Collections.Generic;
using Core.Enum;

namespace LevelBase
{
    public class LevelInfo
    {
        public int LevelNumber;
        public ItemType[,] GridItems;
        public List<LevelGoal> Goals;
        public int NumberOfMoves;
        //public ItemSpawnProbabilityDistributionSO Probability;

        public LevelInfo(int levelNumber, ItemType[,] gridItems, List<LevelGoal> goals, int numberOfMoves)
        {
            LevelNumber = levelNumber;
            GridItems = gridItems;
            Goals = goals;
            NumberOfMoves = numberOfMoves;
        }
    }
}
