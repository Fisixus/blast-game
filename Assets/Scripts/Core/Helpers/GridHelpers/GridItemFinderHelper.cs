using System;
using System.Collections.Generic;
using System.Linq;
using Core.GridElements.GridPawns;
using Core.Helpers.Enums;
using UnityEngine;

namespace Core.Helpers.GridHelpers
{
    public static class GridItemFinderHelper
    {
        public static bool HasStationaryAbove(BaseGridObject[,] grid, int column, int row)
        {
            for (int checkRow = row - 1; checkRow >= 0; checkRow--)
            {
                if (grid[column, checkRow].IsStationary)
                {
                    return true;
                }
            }

            return false;
        }
        
        public static List<BaseGridObject> GetFirstXEmptyGridObjectsByRow(BaseGridObject[,] grid, int matchCount)
        {
            var baseGridObjects = new List<BaseGridObject>(matchCount);

            for (var i = 0; i < grid.GetLength(1); i++)
            for (var j = 0; j < grid.GetLength(0); j++)
            {
                if (grid[j, i].IsEmpty)
                {
                    baseGridObjects.Add(grid[j, i]);
                    matchCount--;
                }

                if (matchCount == 0) return baseGridObjects;
            }

            return baseGridObjects;
        }
    
        public static List<BaseGridObject>
            FindItemsOnHorizontal(BaseGridObject[,] grid, Vector2Int coordinate)
        {
            var gridObjects = new List<BaseGridObject>();

            var gridWidth = grid.GetLength(0);

            for (var distance = 1; distance < gridWidth; distance++)
            {
                // Check left side
                var leftCol = coordinate.x - distance;
                if (leftCol >= 0)
                {
                    var leftObject = grid[leftCol, coordinate.y];
                    gridObjects.Add(leftObject);
                }

                // Check right side
                var rightCol = coordinate.x + distance;
                if (rightCol < gridWidth)
                {
                    var rightObject = grid[rightCol, coordinate.y];
                    gridObjects.Add(rightObject);
                }
            }

            return gridObjects;
        }

        public static List<BaseGridObject>
            FindItemsOnVertical(BaseGridObject[,] grid, Vector2Int coordinate)
        {
            var gridObjects = new List<BaseGridObject>();

            var gridHeight = grid.GetLength(1);

            for (var distance = 1; distance < gridHeight; distance++)
            {
                // Check above
                var upRow = coordinate.y + distance;
                if (upRow < gridHeight)
                {
                    var upObject = grid[coordinate.x, upRow];
                    gridObjects.Add(upObject);
                }

                // Check below
                var downRow = coordinate.y - distance;
                if (downRow >= 0)
                {
                    var downObject = grid[coordinate.x, downRow];
                    gridObjects.Add(downObject);
                }
            }

            return gridObjects;
        }

        public static List<BaseGridObject> FindItemsInRadiusRange(
            BaseGridObject[,] grid, Vector2Int coord, int bombRadius)
        {
            var affectedObjects = new List<BaseGridObject>();
            int maxX = grid.GetLength(0) - 1;
            int maxY = grid.GetLength(1) - 1;

            // Iterate over each position in the radius
            for (int radius = 0; radius <= bombRadius; radius++)
            {
                for (int x = Mathf.Max(0, coord.x - radius); x <= Mathf.Min(maxX, coord.x + radius); x++)
                {
                    for (int y = Mathf.Max(0, coord.y - radius); y <= Mathf.Min(maxY, coord.y + radius); y++)
                    {
                        var gridObject = grid[x, y];

                        if (gridObject == null || affectedObjects.Contains(gridObject))
                            continue;

                        if (gridObject is Obstacle obstacle)
                        {
                            int remainingLife = obstacle.TakeDamage();
                            if (remainingLife > 0)
                                continue;
                        }

                        affectedObjects.Add(gridObject);
                    }
                }
            }
            return affectedObjects;
        }
        public static (IEnumerable<BaseGridObject> ObstaclesAlive, IEnumerable<BaseGridObject> ObstaclesDead) SeparateObstaclesByLife(List<BaseGridObject> obstacles)
        {
            var groupedObstacles = obstacles
                .OfType<Obstacle>() // Ensure we only consider Obstacle objects
                .ToLookup(obstacle => obstacle.Life > 0);

            return (
                groupedObstacles[true].ToList(),
                groupedObstacles[false].ToList()
            );
        }
    }
}
