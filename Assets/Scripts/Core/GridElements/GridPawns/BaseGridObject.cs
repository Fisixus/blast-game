using Core.Enum;
using Core.GridElements.GridPawns.Effect;
using Core.GridElements.GridPawns.Interface;
using Core.Helpers.GridHelpers;
using DG.Tweening;
using UnityEngine;

namespace Core.GridElements.GridPawns
{
    public abstract class BaseGridObject : MonoBehaviour, IGridObject
    {
        [field: SerializeField] public BoxCollider2D BoxCollider { get; private set; }

        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }

        [field: SerializeField] public BaseGridObjectEffect BaseGridObjectEffect { get; private set; }

        [field: SerializeField] public Vector2Int Coordinate { get; set; }

        public ItemType ItemType { get; protected set; }


        public void SetWorldPosition(Vector2 longestCell, Transform gridTopLeftTr,
            Vector2Int? coordinateOverride = null,
            bool isAnimationOn = false, float animationTime = 0.2f)
        {
            // Use the provided override coordinate or default to the current coordinate
            var targetCoordinate = coordinateOverride ?? Coordinate;

            // Calculate the world position
            var itemPosition = CalculateWorldPosition(longestCell, gridTopLeftTr, targetCoordinate);

            // Apply the position with or without animation
            if (isAnimationOn)
                BaseGridObjectEffect.Shift(itemPosition, animationTime, Ease.OutBack, 0.9f);
            else
                transform.position = itemPosition;
        }

        // Helper Method for Position Calculation
        private Vector3 CalculateWorldPosition(Vector2 longestCell, Transform gridTopLeftTr,
            Vector2Int targetCoordinate)
        {
            var gridTr = gridTopLeftTr.parent;
            var scaleNormalizing = gridTr.localScale.x;

            return GridPositionHelper.CalculateItemWorldPosition(gridTopLeftTr.position, longestCell, targetCoordinate,
                scaleNormalizing);
        }

        // SetAttributes, leveraging IType and polymorphism
        public virtual void SetAttributes(Vector2Int newCoord, ItemType type)
        {
            Coordinate = newCoord;
            ItemType = type;
            name = ToString();
        }

        public void SetSortingOrder(int order)
        {
            SpriteRenderer.sortingOrder = order;
        }

        public abstract override string ToString();
    }
}