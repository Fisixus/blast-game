using System;
using System.Collections.Generic;
using Core.GridElements.GridPawns;
using MVP.Presenters.Handlers.Strategies.Enums;
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
