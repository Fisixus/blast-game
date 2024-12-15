using System;
using Core.GridElements.Data;
using Core.GridElements.Enums;
using Core.GridElements.GridPawns.Effect;
using Core.GridElements.GridPawns.Interface;
using Core.Helpers.GridHelpers;
using DG.Tweening;
using UnityEngine;

namespace Core.GridElements.GridPawns
{
    public abstract class BaseGridObject : MonoBehaviour, IGridObject, IType
    {
        [field: SerializeField] public BoxCollider2D BoxCollider { get; private set; }

        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }

        [field: SerializeField] public BaseGridObjectEffect BaseGridObjectEffect { get; private set; }

        [field: SerializeField] public Vector2Int Coordinate { get; set; }

        public abstract Enum Type { get; protected set; } // Enforced by derived classes to follow IType
        public bool IsStationary { get; set; } = false;

        public bool IsEmpty
        {
            get => _isEmpty;
            set
            {
                _isEmpty = value;
                if (_isEmpty)
                {
                    // If empty, adjust other properties accordingly
                    IsStationary = false;
                    SpriteRenderer.enabled = false;
                    BoxCollider.enabled = false;
                }
                else
                {
                    // If not empty, adjust properties oppositely
                    SpriteRenderer.enabled = true;
                    BoxCollider.enabled = true;
                }
            }
        }

        private bool _isEmpty;

        public void SetWorldPosition(Vector2 longestCell, Transform gridTopLeftTr,
            Vector2Int? coordinateOverride = null,
            bool isAnimationOn = false, float animationTime = 0.2f)
        {
            // Use the provided override coordinate or default to the current coordinate
            var targetCoordinate = coordinateOverride ?? Coordinate;

            // Calculate the world position
            var position = CalculateWorldPosition(longestCell, gridTopLeftTr, targetCoordinate);

            // Apply the position with or without animation
            if (isAnimationOn)
                BaseGridObjectEffect.Shift(position, animationTime, Ease.OutBack, 0.9f);
            else
                transform.position = position;
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
        public void SetAttributes(Vector2Int newCoord, Enum type)
        {
            Coordinate = newCoord;
            Type = type;
            name = ToString();
            SetSortingOrder(-newCoord.y);

            // Update the GridAttributes
            IsEmpty = Type is ItemType.None or BoosterType.None or ObstacleType.None or ComboType.None;
        }

        public void SetSortingOrder(int order)
        {
            SpriteRenderer.sortingOrder = order;
        }

        public virtual void ApplyData(BaseGridObjectDataSO data)
        {
            IsStationary = data.IsStationary;
            var gridObjectWidthHeight = data.GridObjectWidthHeight;
            SpriteRenderer.size = new Vector2(gridObjectWidthHeight.x, gridObjectWidthHeight.y);
            BoxCollider.size = new Vector2(gridObjectWidthHeight.x, gridObjectWidthHeight.y);
        }

        public abstract override string ToString();
    }
}