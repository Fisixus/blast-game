using System.Collections.Generic;
using UnityEngine;

namespace Core.LevelSerialization
{
    public class LevelInfo
    {
        public int LevelNumber { get; private set; }
        public System.Enum[,] GridObjectTypes { get; private set; }
        public Vector2Int GridSize { get; private set; }
        public List<LevelGoal> Goals { get; private set; }
        public int NumberOfMoves { get; private set; }
        //public ItemSpawnProbabilityDistributionSO Probability;

        public LevelInfo(int levelNumber, System.Enum[,] gridItems, List<LevelGoal> goals, int numberOfMoves)
        {
            LevelNumber = levelNumber;
            GridObjectTypes = gridItems;
            GridSize = new Vector2Int(GridObjectTypes.GetLength(0), GridObjectTypes.GetLength(1));
            Goals = goals;
            NumberOfMoves = numberOfMoves;
        }

        public override string ToString()
        {
            return $"LevelNumber:{LevelNumber}, GridSize:{GridSize}, NumberOfMoves:{NumberOfMoves}, Goals:{Goals.Count}";
        }
    }
}
