using Core.Helpers.Enums;
using UnityEngine;

namespace Core.Helpers.GridHelpers
{
    public static class GridPositionHelper
    {
        // Helper method to get new coordinates based on direction
        public static Vector2Int GetNewPositionByDirection(int x, int y, Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return new Vector2Int(x - 1, y); // Move Up
                case Direction.Down:
                    return new Vector2Int(x + 1, y); // Move Down
                case Direction.Left:
                    return new Vector2Int(x, y - 1); // Move Left
                case Direction.Right:
                    return new Vector2Int(x, y + 1); // Move Right
                case Direction.LeftDownDiagonal:
                    return new Vector2Int(x + 1, y - 1); // Move Left Down
                case Direction.LeftUpDiagonal:
                    return new Vector2Int(x - 1, y - 1); // Move Left Up
                case Direction.RightDownDiagonal:
                    return new Vector2Int(x + 1, y + 1); // Move Right Down
                case Direction.RightUpDiagonal:
                    return new Vector2Int(x - 1, y + 1); // Move Right Up
                default:
                    return new Vector2Int(x, y); // Default case, no movement
            }
        }

        public static Vector3 CalculateItemWorldPosition(Vector3 gridTopLeftPosition, Vector2 longestCell,
            Vector2Int coordinate, float scaleFactor)
        {
            return new Vector3
            (
                gridTopLeftPosition.x + scaleFactor * longestCell.x * (coordinate.x + 1),
                gridTopLeftPosition.y - scaleFactor * longestCell.y * coordinate.y
            );
        }

        // Validity check for grid bounds
        public static bool IsPositionValid(int x, int y, int columnCount, int rowCount)
        {
            return x >= 0 && x < columnCount && y >= 0 && y < rowCount;
        }
    }
}