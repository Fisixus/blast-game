using System;
using System.Collections.Generic;
using Core.GridElements.Enums;
using Core.GridElements.GridPawns;
using Core.Helpers.Enums;
using Core.Helpers.GridHelpers;
using MVP.Presenters.Strategies.Enums;
using MVP.Presenters.Strategies.Interface;
using UnityEngine;

namespace MVP.Presenters.Strategies.Matches
{
    public class VaseMatchStrategy : IMatchStrategy
    {
        public MatchType MatchType => MatchType.Vase;

        public List<BaseGridObject> FindMatches(
            Vector2Int clickedPosition,
            Enum clickedType,
            BaseGridObject[,] grid,
            bool[,] visited,
            int columnCount,
            int rowCount
        )
        {
            var matchedVases = new List<BaseGridObject>(); // To store cleared Vases.
            var processedVases = new HashSet<BaseGridObject>(); // To track damaged Vases.

            // Check all 4 adjacent cells.
            foreach (var direction in new[] { Direction.Up, Direction.Down, Direction.Left, Direction.Right })
            {
                var adjacentPosition =
                    GridPositionHelper.GetNewPositionByDirection(clickedPosition.x, clickedPosition.y, direction);

                // Skip invalid positions (out of grid bounds).
                if (!GridPositionHelper.IsPositionValid(adjacentPosition.x, adjacentPosition.y, columnCount, rowCount))
                    continue;

                // Get the item at the adjacent position.
                var adjacentItem = grid[adjacentPosition.x, adjacentPosition.y];
                if (adjacentItem == null || visited[adjacentPosition.x, adjacentPosition.y])
                    continue;

                visited[adjacentPosition.x, adjacentPosition.y] = true; // Mark this cell as visited.

                // If the item is a Vase, process it.
                if (adjacentItem.Type.Equals(ObstacleType.Vase) && processedVases.Add(adjacentItem))
                {
                    var vase = adjacentItem as Obstacle;
                    if (vase.TakeDamage() <= 0)
                    {
                        matchedVases.Add(adjacentItem);
                    }
                }
            }

            return matchedVases;
        }
    }
}