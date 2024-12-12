using System;
using System.Collections.Generic;
using Core.GridElements.Enums;
using Core.GridElements.UI;
using UnityEngine;

namespace Core.LevelSerialization
{
    public static class LevelSerializer
    {
        public static LevelInfo SerializeToLevelInfo(int level)
        {
            try
            {
                TextAsset jsonFile = Resources.Load<TextAsset>("Levels/level_" + level.ToString("00"));
                string jsonString = jsonFile.text;
                var levelJson = JsonUtility.FromJson<LevelJson>(jsonString);
                var (gridObjectTypes, levelGoals) = ProcessLevelJson(levelJson);
                return new LevelInfo(levelJson.level_number, gridObjectTypes, levelGoals, levelJson.move_count);
            }
            catch (Exception e)
            {
                Debug.Log("JSON error:" + e);
                throw;
            }
        }
        private static (System.Enum[,] GridObjectTypes, List<LevelGoal> Goals) ProcessLevelJson(LevelJson levelJson)
        {
            // Count obstacles for goal data
            int numberOfBoxes = 0;
            int numberOfStones = 0;
            int numberOfVases = 0;

            // Set the grid data
            var gridData = new System.Enum[levelJson.grid_height, levelJson.grid_width];

            int gridIndex = 0;
            for (int i = levelJson.grid_height - 1; i >= 0; --i)
                for (int j = 0; j < levelJson.grid_width; ++j)
                {
                    switch (levelJson.grid[gridIndex++])
                    {
                        // Obstacles
                        case "bo":
                            gridData[i, j] = ObstacleType.Box;
                            ++numberOfBoxes;
                            break;
                        case "s":
                            gridData[i, j] = ObstacleType.Stone;
                            ++numberOfStones;
                            break;
                        case "v":
                            gridData[i, j] = ObstacleType.Vase;
                           ++numberOfVases;
                            break;
                        // Cubes
                        case "b":
                            gridData[i, j] = ItemType.Blue;
                            break;
                        case "g":
                            gridData[i, j] = ItemType.Green;
                            break;
                        case "r":
                            gridData[i, j] = ItemType.Red;
                            break;
                        case "y":
                            gridData[i, j] = ItemType.Yellow;
                            break;
                        case "rand":
                            gridData[i, j] = ((ItemType[]) System.Enum.GetValues(typeof(ItemType)))[UnityEngine.Random.Range(1, 5)];
                            break;
                        //Boosters
                        case "t":
                            gridData[i, j] = BoosterType.Bomb;
                            break;
                        default:
                            gridData[i, j] = ((ItemType[])System.Enum.GetValues(typeof(ItemType)))[UnityEngine.Random.Range(1, 5)];
                            break;
                    }
                }

            // Set the goals data
            var goals = new List<LevelGoal>();
            if (numberOfBoxes != 0) goals.Add(new LevelGoal { ObstacleType = ObstacleType.Box, Count = numberOfBoxes });
            if (numberOfStones != 0) goals.Add(new LevelGoal { ObstacleType = ObstacleType.Stone, Count = numberOfStones });
            if (numberOfVases != 0) goals.Add(new LevelGoal { ObstacleType = ObstacleType.Vase, Count = numberOfVases });

            return (gridData, goals);
        }
    }
}
