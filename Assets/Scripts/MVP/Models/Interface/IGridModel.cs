using System;
using System.Collections.Generic;
using Core.GridElements.GridPawns;

namespace MVP.Models.Interface
{
    public interface IGridModel
    {
        public BaseGridObject[,] Grid { get; } // x:column, y:row
        public int ColumnCount { get; }
        public int RowCount { get; }
        public void Initialize(List<BaseGridObject> gridObjs, int columns, int rows);
        public void UpdateGridObjects(List<BaseGridObject> newGridObjects, bool isAnimationOn);
        public event Action<BaseGridObject> OnGridObjectInitializedEvent;
        public event Action<BaseGridObject, bool> OnGridObjectUpdatedEvent;
    }
}
