using Core.GridElements.GridPawns.Effect;
using UnityEngine;

namespace Core.GridElements.GridPawns.Interface
{
    public interface IGridObject
    {
        BoxCollider2D BoxCollider { get;  }
        SpriteRenderer SpriteRenderer { get; }
        BaseGridObjectEffect BaseGridObjectEffect { get; }
        Vector2Int Coordinate { get; set; }

        void SetWorldPosition(Vector2 longestCell, Transform gridTopLeftTr, Vector2Int? coordinateOverride = null,
            bool isAnimationOn = false, float animationTime = 0.2f);

        void SetAttributes(Vector2Int newCoord, System.Enum type);
    }
}
