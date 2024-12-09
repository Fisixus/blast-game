using System.Collections.Generic;
using Core.Enum;
using Core.Factories.Interface;
using Core.GridElements.GridPawns;
using UnityEngine;

namespace MVP.Presenters.Handlers
{
    public class HintHandler
    {
        private BaseGridObject[,] _grid;
        private MatchHandler _matchHandler;
        private BoosterHandler _boosterHandler;
        

        private List<List<BaseGridObject>> _allMatchableObjs;
        public HintHandler(MatchHandler matchHandler, BoosterHandler boosterHandler)
        {
            _matchHandler = matchHandler;
            _boosterHandler = boosterHandler;
        }
        
        public void Initialize(BaseGridObject[,] grid)
        {
            _grid = grid;
            DetectAndSetHints();
        }

        public void DetectAndSetHints()
        {
            _allMatchableObjs = new List<List<BaseGridObject>>();
            var visited = new HashSet<Vector2Int>();

            for (var col = 0; col < _grid.GetLength(0); col++)
            {
                for (var row = 0; row < _grid.GetLength(1); row++)
                {
                    var coord = new Vector2Int(col, row);

                    // Skip if this coordinate has already been visited
                    if (visited.Contains(coord)) continue;

                    // Find matches for the current grid object
                    var matchedObjs = _matchHandler.FindGridObjectMatches(_grid[col, row]);

                    if (matchedObjs != null && matchedObjs.Count > 0)
                    {
                        _allMatchableObjs.Add(matchedObjs);

                        // Determine booster type for the current match
                        var boosterType = _boosterHandler.IsBoosterCreatable(matchedObjs);

                        // Apply hint sprites and mark matched coordinates as visited
                        foreach (var matchedObj in matchedObjs)
                        {
                            if (matchedObj is Item item)
                            {
                                item.ApplyHintSprite(boosterType);
                            }
                            visited.Add(matchedObj.Coordinate);
                        }
                    }
                }
            }
        }

        public IEnumerable<BaseGridObject> GetSelectedMatchedItems(BaseGridObject touchedItem)
        {
            foreach (var listObjs in _allMatchableObjs)
            {
                foreach (var gridObj in listObjs)
                {
                    if (gridObj.Equals(touchedItem))
                    {
                        return listObjs;
                    }
                }
            }
            return null;
        }

        
        
    }
}
