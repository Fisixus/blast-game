using System;
using System.Collections.Generic;
using Core.GridElements.GridPawns;
using Core.Helpers.GridHelpers;

namespace MVP.Presenters.Handlers
{
    public class GridShiftHandler
    {
        private GridObjectFactoryHandler _gridObjectFactoryHandler;
        public event Action<BaseGridObject> OnGridObjectShiftedEvent;

        public GridShiftHandler(GridObjectFactoryHandler gridObjectFactoryHandler)
        {
            _gridObjectFactoryHandler = gridObjectFactoryHandler;
        }
        
        public List<BaseGridObject> ShiftAndReplace(BaseGridObject[,] grid, int columnCount, int rowCount, List<BaseGridObject> matchedGridObjects)
        {
            // Mark matched items as empty
            GridItemModifierHelper.MarkEmpty(matchedGridObjects);

            // Combine empty items across all columns
            var combinedEmptyItems = new List<BaseGridObject>();

            // Process each column
            for (var col = 0; col < columnCount; col++)
            {
                combinedEmptyItems.AddRange(ProcessColumn(grid, col, rowCount));
            }

            // Generate new items to fill the empty slots
            return _gridObjectFactoryHandler.GenerateNewItems(combinedEmptyItems);
        }

        private List<BaseGridObject> ProcessColumn(BaseGridObject[,] grid, int column, int rowCount)
        {
            var emptyItems = new List<BaseGridObject>();
            var emptyRow = rowCount - 1;

            for (var row = rowCount - 1; row >= 0; row--)
            {
                var currentGridObject = grid[column, row];

                if (currentGridObject.IsStationary)
                {
                    emptyRow = row - 1; // Skip stationary objects and update emptyRow
                }
                else if (!currentGridObject.IsEmpty)
                {
                    ShiftItemDown(grid, column, row, ref emptyRow);
                }
                else if (!GridItemFinderHelper.HasStationaryAbove(grid, column, row))
                {
                    emptyItems.Add(currentGridObject);
                }
            }

            return emptyItems;
        }

        private void ShiftItemDown(BaseGridObject[,] grid, int column, int sourceRow, ref int targetRow)
        {
            if (sourceRow != targetRow)
            {
                GridItemModifierHelper.SwapItems(grid, column, targetRow, column, sourceRow);
                OnGridObjectShiftedEvent?.Invoke(grid[column, targetRow]);
            }

            targetRow--;
        }

        




    }
}
