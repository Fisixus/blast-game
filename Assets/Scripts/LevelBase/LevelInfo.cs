using System;
using System.Collections.Generic;
using Core.Enum;
using UnityEngine;

namespace LevelBase
{
    public class LevelInfo
    {
        public int LevelNumber { get; private set; }
        public ItemType[,] GridItems { get; private set; }
        public Vector2Int GridSize { get; private set; }
        public List<LevelGoal> Goals { get; private set; }
        public int NumberOfMoves { get; private set; }
        //public ItemSpawnProbabilityDistributionSO Probability;

        public LevelInfo(int levelNumber, ItemType[,] gridItems, List<LevelGoal> goals, int numberOfMoves)
        {
            LevelNumber = levelNumber;
            GridItems = gridItems;
            GridSize = new Vector2Int(GridItems.GetLength(0), GridItems.GetLength(1));
            Goals = goals;
            NumberOfMoves = numberOfMoves;
        }
    }
}
