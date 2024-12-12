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
    public class BoosterMatchStrategy : IMatchStrategy
    {
        public MatchType MatchType => MatchType.Booster;

        private const int MinMatchCount = 2;
        
        private readonly List<BaseGridObject> _matchedBoosters = new();

        public List<BaseGridObject> FindMatches(
            Vector2Int clickedPosition,
            Enum clickedType,
            BaseGridObject[,] grid,
            bool[,] visited,
            int columnCount,
            int rowCount
        )
        {
            _matchedBoosters.Clear();
            if (clickedType is not BoosterType)
            {
                return new List<BaseGridObject>();
            }
            FindMatchingBoosters(clickedPosition, grid, visited, columnCount, rowCount);
            if (!IsValidMatch()) return new List<BaseGridObject>();
            return _matchedBoosters;
        }

        // Recursive function to find all adjacent matching items
        private void FindMatchingBoosters(Vector2Int position, BaseGridObject[,] grid, bool[,] visited,
            int columnCount, int rowCount)
        {
            var x = position.x;
            var y = position.y;

            // Exit if the position is invalid, already visited, or item type does not match
            if (!GridPositionHelper.IsPositionValid(x, y, columnCount, rowCount) || visited[x, y]) return;
            var currentBooster = grid[x, y] as Booster;
            if (currentBooster == null) return;

            // Mark the current cell as visited and add it to matched items
            visited[x, y] = true;
            _matchedBoosters.Add(currentBooster);

            // Explore valid neighboring cells
            foreach (var direction in new[] { Direction.Up, Direction.Down, Direction.Left, Direction.Right })
            {
                var newPosition = GridPositionHelper.GetNewPositionByDirection(x, y, direction);
                FindMatchingBoosters(newPosition, grid, visited, columnCount, rowCount);
            }
        }

        private bool IsValidMatch() => _matchedBoosters.Count >= MinMatchCount;
    }
}
