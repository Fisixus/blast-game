using System;
using System.Collections.Generic;
using Core.GridElements.Enums;
using Core.GridElements.UI;
using UnityEngine;
using Random = UnityEngine.Random;

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
        public static (Enum[,] GridObjectTypes, List<LevelGoal> Goals) ProcessLevelJson(LevelJson levelJson)
        {
            // Count obstacles for goal data
            int numberOfBoxes = 0;
            int numberOfStones = 0;
            int numberOfVases = 0;

            // Set the grid data
            var gridData = new Enum[levelJson.grid_height, levelJson.grid_width];

            int gridIndex = 0;
            for (int i = levelJson.grid_height - 1; i >= 0; --i)
                for (int j = 0; j < levelJson.grid_width; ++j)
                {
                    switch (levelJson.grid[gridIndex++])
                    {
                        // Obstacles
                        case nameof(JsonGridObjectType.bo):
                            gridData[i, j] = ObstacleType.Box;
                            ++numberOfBoxes;
                            break;
                        case nameof(JsonGridObjectType.s):
                            gridData[i, j] = ObstacleType.Stone;
                            ++numberOfStones;
                            break;
                        case nameof(JsonGridObjectType.v):
                            gridData[i, j] = ObstacleType.Vase;
                           ++numberOfVases;
                            break;
                        // Cubes
                        case nameof(JsonGridObjectType.b):
                            gridData[i, j] = ItemType.Blue;
                            break;
                        case nameof(JsonGridObjectType.g):
                            gridData[i, j] = ItemType.Green;
                            break;
                        case nameof(JsonGridObjectType.r):
                            gridData[i, j] = ItemType.Red;
                            break;
                        case nameof(JsonGridObjectType.y):
                            gridData[i, j] = ItemType.Yellow;
                            break;
                        case nameof(JsonGridObjectType.rand):
                            gridData[i, j] = ((ItemType[]) Enum.GetValues(typeof(ItemType)))[Random.Range(1, 5)];
                            break;
                        //Boosters
                        case nameof(JsonGridObjectType.t):
                            gridData[i, j] = BoosterType.Bomb;
                            break;
                        default:
                            gridData[i, j] = ((ItemType[])Enum.GetValues(typeof(ItemType)))[Random.Range(1, 5)];
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
        
        public static LevelJson ConvertToLevelJson(int gridWidth, int gridHeight, int moveCount, JsonGridObjectType[,] items)
        {
            LevelJson levelJson = new LevelJson
            {
                level_number = 0,
                grid_width = gridWidth,
                grid_height = gridHeight,
                move_count = moveCount,
                grid = new string[gridWidth * gridHeight]
            };

            for (int x = 0; x < gridHeight; x++)
            {
                for (int y = 0; y < gridWidth; y++)
                {
                    // Reverse the row order but keep elements in the same order within each row
                    int reversedRow = gridHeight - 1 - x;
                    levelJson.grid[reversedRow * gridWidth + y] = items[x, y].ToString();
                }
            }

            return levelJson;
        }



    }
}
