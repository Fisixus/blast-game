using Core.Enum;
using Core.GridElements.GridPawns.Effect;
using UnityEngine;

namespace Core.GridElements.GridPawns.Interface
{
    public interface IGridObject
    {
        SpriteRenderer SpriteRenderer { get; }
        BaseGridObjectEffect BaseGridObjectEffect { get; }
        Vector2Int Coordinate { get; set; }
        
        public struct GridObjectAttributes
        {
            public bool IsEmpty { get; set; }
            public bool IsStationary { get; set; }
            public int HitCount { get; set; }

            public void SetAttributes(System.Enum type)
            {
                IsEmpty = type is ItemType.None or BoosterType.None;
                IsStationary = type is ItemType.SI_Box or ItemType.SI_Stone;
                HitCount = type is ItemType.SI_Vase ? 2 : 1;
            }

            public override string ToString()
            {
                return $"IsEmpty: {IsEmpty}, IsStationary: {IsStationary}, HitCount: {HitCount}";
            }
        }
        public GridObjectAttributes Attributes { get; }

        void SetWorldPosition(Vector2 longestCell, Transform gridTopLeftTr, Vector2Int? coordinateOverride = null,
            bool isAnimationOn = false, float animationTime = 0.2f);

        void SetAttributes(Vector2Int newCoord, System.Enum type);
    }
}
