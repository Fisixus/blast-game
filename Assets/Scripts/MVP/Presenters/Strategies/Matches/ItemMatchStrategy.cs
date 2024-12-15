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
    public class ItemMatchStrategy : IMatchStrategy
    {
        public MatchType MatchType => MatchType.Item;

        private const int MinMatchCount = 2;

        private readonly List<BaseGridObject> _matchedItems = new();

        public List<BaseGridObject> FindMatches(
            Vector2Int clickedPosition,
            Enum clickedType,
            BaseGridObject[,] grid,
            bool[,] visited,
            int columnCount,
            int rowCount
        )
        {
            _matchedItems.Clear();
            if (clickedType is not ItemType)
            {
                return new List<BaseGridObject>();
            }

            FindMatchingItems(clickedPosition, clickedType, grid, visited, columnCount, rowCount);
            if (!IsValidMatch()) return new List<BaseGridObject>();
            return _matchedItems;
        }

        // Recursive function to find all adjacent matching items
        private void FindMatchingItems(Vector2Int position, Enum itemType, BaseGridObject[,] grid, bool[,] visited,
            int columnCount, int rowCount)
        {
            var x = position.x;
            var y = position.y;

            // Exit if the position is invalid, already visited, or item type does not match
            if (!GridPositionHelper.IsPositionValid(x, y, columnCount, rowCount) || visited[x, y]) return;
            var currentItem = grid[x, y] as Item;
            if (currentItem == null || !currentItem.ItemType.Equals(itemType)) return;

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

        private bool IsValidMatch() => _matchedItems.Count >= MinMatchCount;
    }
}