using System.Collections.Generic;
using Core.Enum;
using Core.GridElements.GridPawns;
using Core.Helpers.GridHelpers;
using MVP.Presenters.Handlers.Strategies.Interface;
using UnityEngine;

namespace MVP.Presenters.Handlers.Strategies.Match
{
    public class BoxMatchStrategy : IMatchStrategy
    {
        public MatchType MatchType => MatchType.Box;

        public List<Item> FindMatches(
            Vector2Int clickedPosition,
            ItemType clickedItemType,
            BaseGridObject[,] grid,
            bool[,] visited,
            int columnCount,
            int rowCount
        )
        {
            // Boxes are always found adjacent to other matched items, not as standalone matches.
            var matchedBoxes = new List<Item>();

            // Explore all directions around the clicked position for adjacent boxes.
            foreach (var direction in new[] { Direction.Up, Direction.Down, Direction.Left, Direction.Right })
            {
                var adjacentPosition =
                    GridPositionHelper.GetNewPositionByDirection(clickedPosition.x, clickedPosition.y, direction);

                if (!GridPositionHelper.IsPositionValid(adjacentPosition.x, adjacentPosition.y, columnCount, rowCount))
                    continue;

                var adjacentItem = grid[adjacentPosition.x, adjacentPosition.y] as Item;
                if (adjacentItem == null || adjacentItem.ItemType != ItemType.SI_Box) continue;
            
                matchedBoxes.Add(adjacentItem);
                visited[adjacentPosition.x, adjacentPosition.y] = true;
            }

            return matchedBoxes;
        }
    
    
    }
}
