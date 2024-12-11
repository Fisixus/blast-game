using System;
using System.Collections.Generic;
using System.Linq;
using Core.GridElements.GridPawns;
using MVP.Presenters.Handlers.Strategies.Enums;
using MVP.Presenters.Handlers.Strategies.Interface;
using UnityEngine;

namespace MVP.Presenters.Handlers
{
    public class MatchHandler
    {
        private readonly Dictionary<MatchType, IMatchStrategy> _strategies = new();
        
        private bool[,] _visited; // To track visited items
        private int _rowCount;
        private int _columnCount;
        
        private BaseGridObject[,] _grid;
        
        public MatchHandler(IEnumerable<IMatchStrategy> strategies)
        {
            foreach (var strategy in strategies)
            {
                _strategies[strategy.MatchType] = strategy;
            }
        }
        
        public void Initialize(BaseGridObject[,] grid)
        {
            _grid = grid;
            _columnCount = _grid.GetLength(0);
            _rowCount = _grid.GetLength(1);
            _visited = new bool[_columnCount, _rowCount];
        }
        
        public List<BaseGridObject> FindGridObjectMatches(BaseGridObject gridObject)
        {
            ClearVisited(_visited, _columnCount, _rowCount);
            var matchedObjs = new List<BaseGridObject>();

            if (_strategies.TryGetValue(MatchType.GridObject, out var strategyItem))
            {
                matchedObjs.AddRange(strategyItem.FindMatches(gridObject.Coordinate, gridObject.Type, _grid, _visited,
                    _columnCount, _rowCount));
            }
            
            return matchedObjs;
        }

        public List<BaseGridObject> FindObstacles(List<BaseGridObject> matchedObjs)
        {
            var obstacles = new List<BaseGridObject>();
            var matchTypesToHandle = new[] { MatchType.Box, MatchType.Vase };

            foreach (var matchedObj in matchedObjs)
            {
                foreach (var matchType in matchTypesToHandle)
                {
                    if (_strategies.TryGetValue(matchType, out var strategy))
                    {
                        obstacles.AddRange(strategy.FindMatches(
                            matchedObj.Coordinate,
                            matchedObj.Type,
                            _grid,
                            _visited,
                            _columnCount,
                            _rowCount));
                    }
                }
            }
            
            return obstacles;
        }

        private void ClearVisited(bool[,] visited, int columnCount, int rowCount)
        {
            for (var i = 0; i < columnCount; i++)
            for (var j = 0; j < rowCount; j++)
                visited[i, j] = false;
        }
    }
}
