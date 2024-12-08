using System.Collections.Generic;
using Core.Enum;
using Core.GridElements.GridPawns;
using Core.Helpers.GridHelpers;
using MVP.Presenters.Handlers.Strategies.Interface;
using UnityEngine;

namespace MVP.Presenters.Handlers.Strategies.Match
{
    public class ItemMatchStrategy : IMatchStrategy
    {
        public MatchType MatchType => MatchType.RegularItems;

        private const int MinMatchCount = 2;

        private readonly List<ItemType> _matchableTypes = new()
        {
            ItemType.RI_Red,
            ItemType.RI_Green,
            ItemType.RI_Blue,
            ItemType.RI_Yellow
        };

        private readonly List<Item> _matchedItems = new();

        public List<Item> FindMatches(
            Vector2Int clickedPosition,
            ItemType clickedItemType,
            BaseGridObject[,] grid,
            bool[,] visited,
            int columnCount,
            int rowCount
        )
        {
            _matchedItems.Clear();
            if (!IsMatchableType(clickedItemType))
                return new List<Item>();

            FindMatchingItems(clickedPosition, clickedItemType, grid, visited, columnCount, rowCount);

            if (!IsValidMatch()) return new List<Item>();

            return _matchedItems;
        }

        // Recursive function to find all adjacent matching items
        private void FindMatchingItems(Vector2Int position, ItemType itemType, BaseGridObject[,] grid, bool[,] visited,
            int columnCount, int rowCount)
        {
            var x = position.x;
            var y = position.y;

            // Exit if the position is invalid, already visited, or item type does not match
            if (!GridPositionHelper.IsPositionValid(x, y, columnCount, rowCount) || visited[x, y]) return;

            var currentItem = grid[x, y] as Item;
            if (currentItem == null || currentItem.ItemType != itemType) return;

            // Mark the current cell as visited and add it to matched items
            visited[x, y] = true;
            _matchedItems.Add(currentItem);

            // Explore valid neighboring cells
            foreach (var direction in new[] { Direction.Up, Direction.Down, Direction.Left, Direction.Right })
            {
                var newPosition = GridPositionHelper.GetNewPositionByDirection(x, y, direction);
                FindMatchingItems(newPosition, itemType, grid, visited, columnCount, rowCount);
            }
        }

        private bool IsMatchableType(ItemType itemType) => _matchableTypes.Contains(itemType);

        private bool IsValidMatch() => _matchedItems.Count >= MinMatchCount;
    }
}
