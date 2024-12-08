using System.Collections.Generic;
using Core.Enum;
using Core.GridElements.GridPawns;
using MVP.Presenters.Handlers.Strategies.Interface;

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
        
        // Finds regular matches and nearby balloons
        public List<Item> FindMatches(Item item)
        {
            ClearVisited(_visited, _columnCount, _rowCount);
            var matchedItems = new List<Item>();

            if (_strategies.TryGetValue(MatchType.RegularItems, out var strategy))
            {
                matchedItems.AddRange(strategy.FindMatches(item.Coordinate, item.ItemType, _grid, _visited,
                    _columnCount, _rowCount));
            }
            

            return matchedItems;
        }
        
        private void ClearVisited(bool[,] visited, int columnCount, int rowCount)
        {
            for (var i = 0; i < columnCount; i++)
            for (var j = 0; j < rowCount; j++)
                visited[i, j] = false;
        }
    }
}
