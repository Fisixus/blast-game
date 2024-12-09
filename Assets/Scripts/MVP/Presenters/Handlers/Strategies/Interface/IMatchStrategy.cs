using System;
using System.Collections.Generic;
using Core.Enum;
using Core.GridElements.GridPawns;
using UnityEngine;

namespace MVP.Presenters.Handlers.Strategies.Interface
{
    public interface IMatchStrategy
    {
        public MatchType MatchType { get; }

        List<BaseGridObject> FindMatches(
            Vector2Int clickedPosition,
            Enum clickedType,
            BaseGridObject[,] grid,
            bool[,] visited,
            int columnCount,
            int rowCount
        );
    }
}
