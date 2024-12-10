using System.Collections.Generic;
using Core.Factories.Interface;
using Core.GridElements.GridPawns;
using Core.Helpers.GridHelpers;
using Events;
using Events.Grid;

namespace MVP.Presenters.Handlers
{
    public class GridShiftHandler
    {
        private GridObjectFactoryHandler _gridObjectFactoryHandler;

        public GridShiftHandler(GridObjectFactoryHandler gridObjectFactoryHandler)
        {
            _gridObjectFactoryHandler = gridObjectFactoryHandler;
        }
        
        public List<BaseGridObject> ShiftAndReplace(BaseGridObject[,] grid, int columnCount, int rowCount, List<BaseGridObject> matchedGridObjects)
        {
            // Mark matched items as empty
            GridItemModifierHelper.MarkEmpty(matchedGridObjects);

            // Shift existing items downward
            var emptyItems = ShiftItems(grid, columnCount, rowCount);

            // Generate new items to fill empty slots
            var newItems = _gridObjectFactoryHandler.GenerateNewItems(emptyItems);

            return newItems;
        }
        
        private List<BaseGridObject> ShiftItems(BaseGridObject[,] grid, int columnCount, int rowCount)
        {
            var emptyItems = new List<BaseGridObject>();

            for (var col = 0; col < columnCount; col++)
            {
                var emptyRow = rowCount - 1;

                for (var row = rowCount - 1; row >= 0; row--)
                {
                    if (!grid[col, row].IsEmpty)
                    {
                        var baseGridObject = grid[col, row];
                        GridItemModifierHelper.SwapItems(grid, col, emptyRow, col, row);
                        
                        GameEventSystem.Invoke<OnGridObjectShiftedEvent>
                            (new OnGridObjectShiftedEvent(){ GridObject = baseGridObject});
                        
                        emptyRow--;
                    }
                    else
                    {
                        emptyItems.Add(grid[col, row]);
                    }
                }
            }

            return emptyItems;
        }

    }
}
