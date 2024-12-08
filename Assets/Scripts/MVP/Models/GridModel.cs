using System.Collections.Generic;
using Core.GridElements.GridPawns;
using MVP.Models.Interface;
using UnityEngine;

namespace MVP.Models
{
    public class GridModel : IGridModel
    {
        public BaseGridObject[,] Grid { get; private set; } // x:column, y:row
        public int ColumnCount { get; private set; }
        public int RowCount { get; private set; }
        
        
        public void Initialize(List<BaseGridObject> gridObjs, int columns, int rows)
        {
            ColumnCount = columns;
            RowCount = rows;
            Grid = new BaseGridObject[ColumnCount, RowCount];
            for (var i = 0; i < ColumnCount; i++)
            for (var j = 0; j < RowCount; j++)
            {
                Grid[i, j] = gridObjs[i * RowCount + j];
                //TODO: m_SignalBus.Fire(new OnGridObjectInitializedSignal() { InitializedBaseGridObject = Grid[i, j] });
            }
        }
    }
}
