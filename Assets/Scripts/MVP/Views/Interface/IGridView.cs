
using UnityEngine;

namespace MVP.Views.Interface
{
    public interface IGridView
    {
        public SpriteRenderer GridSprite { get; }

        public Transform GridTopLeftTr { get; }

        public Vector2 GridTopLeftMargin { get; }

        public Vector2 GridPadding { get; }

        public void CalculateGridSize(Vector2Int gridSize, Vector2 longestCell);

        public void ScaleGrid();
    }
}
