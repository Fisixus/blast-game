using System.Collections.Generic;
using Core.GridElements.Enums;
using Core.GridElements.GridPawns;
using UnityEngine;

namespace Core.Helpers.GridHelpers
{
    public static class GridItemModifierHelper
    {
        public static void MarkEmpty(IEnumerable<BaseGridObject> matchedItems)
        {
            foreach (var baseGridObject in matchedItems)
            {
                baseGridObject.IsEmpty = true;
                switch (baseGridObject)
                {
                    case Item item:
                        item.ItemType = ItemType.None;
                        break;
                    case Booster booster:
                        booster.BoosterType = BoosterType.None;
                        break;
                    case Obstacle obstacle:
                        obstacle.ObstacleType = ObstacleType.None;
                        break;
                    case Combo combo:
                        combo.ComboType = ComboType.None;
                        break;
                }
            }
        }

        public static void SwapItems(BaseGridObject[,] grid, int x1, int y1, int x2, int y2)
        {
            // Swap the references in the grid array
            (grid[x1, y1], grid[x2, y2]) = (grid[x2, y2], grid[x1, y1]);

            // Update the coordinates and world positions for both items
            grid[x1, y1].SetAttributes(new Vector2Int(x1, y1), grid[x1, y1].Type);
            grid[x2, y2].SetAttributes(new Vector2Int(x2, y2), grid[x2, y2].Type);
        }
    }
}