using System.Collections.Generic;
using System.Linq;
using Core.GridElements.Enums;
using Core.GridElements.GridPawns;
using Unity.VisualScripting;
using UnityEngine;

namespace MVP.Presenters.Handlers
{
    public class HintHandler
    {
        private readonly MatchHandler _matchHandler;
        private readonly BoosterHandler _boosterHandler;
        
        private BaseGridObject[,] _grid;
        private int _rowCount;
        private int _columnCount;
        
        

        private List<List<BaseGridObject>> _allMatchableObjs = new();
        public HintHandler(MatchHandler matchHandler, BoosterHandler boosterHandler)
        {
            _matchHandler = matchHandler;
            _boosterHandler = boosterHandler;
        }
        
        public void Initialize(BaseGridObject[,] grid)
        {
            _grid = grid;
            _columnCount = _grid.GetLength(0);
            _rowCount = _grid.GetLength(1);
            DetectAndSetHints();
        }

        public void DetectAndSetHints()
        {
            ClearHintSprites(); // Clear existing hints
            _allMatchableObjs = new List<List<BaseGridObject>>();
            var visited = new HashSet<Vector2Int>();

            for (var col = 0; col < _columnCount; col++)
            {
                for (var row = 0; row < _rowCount; row++)
                {
                    var gridObject = _grid[col, row];

                    // Skip obstacles and already visited coordinates
                    if (ShouldSkipObject(gridObject, visited, col, row)) 
                        continue;

                    ProcessMatches(gridObject, visited);
                }
            }
        }

        private bool ShouldSkipObject(BaseGridObject gridObject, HashSet<Vector2Int> visited, int col, int row)
        {
            return gridObject is Obstacle || visited.Contains(new Vector2Int(col, row));
        }

        private void ProcessMatches(BaseGridObject gridObject, HashSet<Vector2Int> visited)
        {
            // Find item matches
            var matchedItems = _matchHandler.FindItemMatches(gridObject);
            if (matchedItems is { Count: > 0 })
            {
                _allMatchableObjs.Add(matchedItems);

                // Determine booster type and apply hints
                var boosterType = _boosterHandler.IsBoosterCreatable(matchedItems.Cast<Item>());
                ApplyHintSpritesAndMarkVisited(matchedItems, boosterType, visited);
            }

            // Find booster matches
            var matchedBoosters = _matchHandler.FindBoosterMatches(gridObject);
            if (matchedBoosters is { Count: > 0 })
            {
                _allMatchableObjs.Add(matchedBoosters);
                visited.UnionWith(matchedBoosters.Select(m => m.Coordinate));
            }
        }

        private void ApplyHintSpritesAndMarkVisited(IEnumerable<BaseGridObject> matchedObjects, BoosterType boosterType, HashSet<Vector2Int> visited)
        {
            foreach (var matchedObj in matchedObjects)
            {
                if (matchedObj is Item item)
                {
                    item.ApplyHintSprite(boosterType);
                }

                visited.Add(matchedObj.Coordinate);
            }
        }
        public IEnumerable<BaseGridObject> GetSelectedMatchedGridObjects(BaseGridObject touchedObj)
        {
            foreach (var listObjs in _allMatchableObjs)
            {
                foreach (var gridObj in listObjs)
                {
                    if (gridObj.Equals(touchedObj))
                    {
                        return listObjs;
                    }
                }
            }
            return new List<BaseGridObject>();
        }
        
        private void ClearHintSprites()
        {
            foreach (var listObjs in _allMatchableObjs)
            {
                foreach (var gridObj in listObjs)
                {
                    if (gridObj is Item item)
                    {
                        item.ApplyHintSprite(BoosterType.None);
                    }
                }
            }
        }
    }
}
