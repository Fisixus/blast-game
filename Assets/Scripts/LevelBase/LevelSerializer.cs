using System;
using System.Collections.Generic;
using Core.Enum;
using UnityEngine;

namespace LevelBase
{
    public static class LevelSerializer
    {
        public static LevelInfo SerializeToLevelInfo(int level)
        {
            try
            {
                TextAsset jsonFile = Resources.Load<TextAsset>("Levels/level_" + level.ToString("00"));
                string jsonString = jsonFile.text;
                var levelJson = JsonUtility.FromJson<LevelJSON>(jsonString);
                var (gridItems, levelGoals) = ProcessLevelJson(levelJson);
                return new LevelInfo(gridItems, levelGoals, levelJson.MoveCount);
            }
            catch (Exception e)
            {
                Debug.Log(e);
                throw;
            }
        }
        private static (ItemType[,] GridItems, List<LevelGoal> Goals) ProcessLevelJson(LevelJSON levelJson)
        {
            // Count obstacles for goal data
            int numberOfBoxes = 0;
            int numberOfStones = 0;
            int numberOfVases = 0;

            // Set the grid data
            var gridData = new ItemType[levelJson.GridHeight, levelJson.GridWidth];

            int gridIndex = 0;
            for (int i = levelJson.GridHeight - 1; i >= 0; --i)
                for (int j = 0; j < levelJson.GridWidth; ++j)
                {
                    switch (levelJson.Grid[gridIndex++])
                    {
                        // Obstacles
                        case "bo":
                            gridData[i, j] = ItemType.SI_Box;
                            ++numberOfBoxes;
                            break;
                        case "s":
                            gridData[i, j] = ItemType.SI_Stone;
                            ++numberOfStones;
                            break;
                        case "v":
                            gridData[i, j] = ItemType.SI_Vase;
                           ++numberOfVases;
                            break;
                        // Cubes
                        case "b":
                            gridData[i, j] = ItemType.RI_Blue;
                            break;
                        case "g":
                            gridData[i, j] = ItemType.RI_Green;
                            break;
                        case "r":
                            gridData[i, j] = ItemType.RI_Red;
                            break;
                        case "y":
                            gridData[i, j] = ItemType.RI_Yellow;
                            break;
                        case "rand":
                            gridData[i, j] = ((ItemType[]) Enum.GetValues(typeof(ItemType)))[UnityEngine.Random.Range(1, 5)];
                            break;
                        case "t":
                            gridData[i, j] = ItemType.Bomb;
                            break;
                        default:
                            gridData[i, j] = ((ItemType[])Enum.GetValues(typeof(ItemType)))[UnityEngine.Random.Range(1, 5)];
                            break;
                    }
                }

            // Set the goals data
            var goals = new List<LevelGoal>();
            if (numberOfBoxes != 0) goals.Add(new LevelGoal { ItemType = ItemType.SI_Box, Count = numberOfBoxes });
            if (numberOfStones != 0) goals.Add(new LevelGoal { ItemType = ItemType.SI_Stone, Count = numberOfStones });
            if (numberOfVases != 0) goals.Add(new LevelGoal { ItemType = ItemType.SI_Vase, Count = numberOfVases });

            return (gridData, goals);
        }
    }
}
