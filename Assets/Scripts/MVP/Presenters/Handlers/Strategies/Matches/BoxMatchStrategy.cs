using System;
using System.Collections.Generic;
using Core.GridElements.Enums;
using Core.GridElements.GridPawns;
using Core.Helpers.Enums;
using Core.Helpers.GridHelpers;
using MVP.Presenters.Handlers.Strategies.Enums;
using MVP.Presenters.Handlers.Strategies.Interface;
using UnityEngine;

namespace MVP.Presenters.Handlers.Strategies.Matches
{
    public class BoxMatchStrategy : IMatchStrategy
    {
        public MatchType MatchType => MatchType.Box;

        public List<BaseGridObject> FindMatches(
            Vector2Int clickedPosition,
            Enum clickedType,
            BaseGridObject[,] grid,
            bool[,] visited,
            int columnCount,
            int rowCount
        )
        {
            // Boxes are always found adjacent to other matched items, not as standalone matches.
            var matchedBoxes = new List<BaseGridObject>();

            // Explore all directions around the clicked position for adjacent boxes.
            foreach (var direction in new[] { Direction.Up, Direction.Down, Direction.Left, Direction.Right })
            {
                var adjacentPosition =
                    GridPositionHelper.GetNewPositionByDirection(clickedPosition.x, clickedPosition.y, direction);

                if (!GridPositionHelper.IsPositionValid(adjacentPosition.x, adjacentPosition.y, columnCount, rowCount))
                    continue;

                var adjacentItem = grid[adjacentPosition.x, adjacentPosition.y];
                if (adjacentItem == null || !adjacentItem.Type.Equals(ObstacleType.Box)) continue;
            
                matchedBoxes.Add(adjacentItem);
                visited[adjacentPosition.x, adjacentPosition.y] = true;
            }

            return matchedBoxes;
        }
    
    
    }
}
