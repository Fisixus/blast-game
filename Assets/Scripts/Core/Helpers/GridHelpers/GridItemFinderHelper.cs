using System.Collections.Generic;
using System.Linq;
using Core.Enum;
using Core.GridElements.GridPawns;
using UnityEngine;

namespace Core.Helpers.GridHelpers
{
    public static class GridItemFinderHelper
    {
        public static List<BaseGridObject> GetFirstXEmptyGridObjectsByRow(BaseGridObject[,] grid, int matchCount)
        {
            var baseGridObjects = new List<BaseGridObject>(matchCount);

            for (var i = 0; i < grid.GetLength(1); i++)
            for (var j = 0; j < grid.GetLength(0); j++)
            {
                if (grid[j, i].Attributes.IsEmpty)
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

        public static List<BaseGridObject> FindItemsInCircleRange(BaseGridObject[,] grid, Vector2Int coord, int bombRadius)
        {
            var effectedObjects = new List<BaseGridObject>();
            var maxX = grid.GetLength(0) - 1; // Maximum X index
            var maxY = grid.GetLength(1) - 1; // Maximum Y index

            // Calculate bounds of the explosion area
            var startX = Mathf.Max(0, coord.x - bombRadius);
            var endX = Mathf.Min(maxX, coord.x + bombRadius);
            var startY = Mathf.Max(0, coord.y - bombRadius);
            var endY = Mathf.Min(maxY, coord.y + bombRadius);

            // Iterate through the NxN area
            for (var x = startX; x <= endX; x++)
            {
                for (var y = startY; y <= endY; y++)
                {
                    if (grid[x, y] != null && !effectedObjects.Contains(grid[x, y]))
                    {
                        effectedObjects.Add(grid[x, y]);
                    }
                }
            }
            return effectedObjects;
        }
        
        public static (List<Item> Obstacles, List<Item> RegularItems) SeparateRegularItems(List<Item> matchedItems)
        {
            var groupedItems = matchedItems.ToLookup(i => i.ItemType is ItemType.SI_Box or ItemType.SI_Stone or ItemType.SI_Vase);
            return (groupedItems[true].ToList(), groupedItems[false].ToList());
        }
    }
}
