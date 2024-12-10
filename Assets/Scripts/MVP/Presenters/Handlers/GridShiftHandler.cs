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
        private readonly IItemFactory _itemFactory;
        private readonly IBoosterFactory _boosterFactory;

        public GridShiftHandler(IItemFactory itemFactory, IBoosterFactory boosterFactory)
        {
            _itemFactory = itemFactory;
            _boosterFactory = boosterFactory;
        }
        
        public List<BaseGridObject> ShiftAndReplace(BaseGridObject[,] grid, int columnCount, int rowCount, List<BaseGridObject> matchedGridObjects)
        {
            // Mark matched items as empty
            GridItemModifierHelper.MarkEmpty(matchedGridObjects);

            // Shift existing items downward
            var emptyItems = ShiftItems(grid, columnCount, rowCount);

            // Generate new items to fill empty slots
            var newItems = GenerateNewItems(emptyItems);

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
                        
                        //TODO:GameEventSystem.Invoke<OnGridObjectInitializedEvent>
                            //(new OnGridObjectShiftedEvent() { GridObject = baseGridObject });
                        
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
        //TODO:
        private List<BaseGridObject> GenerateNewItems(List<BaseGridObject> emptyItems)
        {
            var newItems = new List<BaseGridObject>();

            foreach (var emptyItem in emptyItems)
            {
                newItems.Add(_itemFactory.GenerateRandItem(emptyItem.Coordinate));
                switch (emptyItem)
                {
                    case Item item:
                        _itemFactory.DestroyObj(item);
                        break;
                    case Booster booster:
                        _boosterFactory.DestroyObj(booster);
                        break;
                }
            }

            return newItems;
        }
    }
}
