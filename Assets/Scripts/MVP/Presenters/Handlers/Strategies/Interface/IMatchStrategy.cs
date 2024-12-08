using System.Collections.Generic;
using Core.Enum;
using Core.GridElements.GridPawns;
using UnityEngine;

namespace MVP.Presenters.Handlers.Strategies.Interface
{
    public interface IMatchStrategy
    {
        public MatchType MatchType { get; }

        List<Item> FindMatches(
            Vector2Int clickedPosition,
            ItemType clickedItemType,
            BaseGridObject[,] grid,
            bool[,] visited,
            int columnCount,
            int rowCount
        );
    }
}
