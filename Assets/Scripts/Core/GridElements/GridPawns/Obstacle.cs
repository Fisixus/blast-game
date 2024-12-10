using Core.Enum;
using Core.GridObjectsData;
using UnityEngine;

namespace Core.GridElements.GridPawns
{
    public class Obstacle : BaseGridObject
    {
        [field: SerializeField] public ObstacleType ObstacleType { get; set; }

        public struct ObstacleAttributes
        {
            public bool IsStationary { get; set; }
            public int HitCount { get; set; }

            public void SetAttributes(System.Enum type)
            {
                IsStationary = type is ObstacleType.Box or Enum.ObstacleType.Stone;
                HitCount = type is ObstacleType.Vase ? 2 : 1;
            }

            public override string ToString()
            {
                return $"IsStationary: {IsStationary}, HitCount: {HitCount}";
            }
        }
        public ObstacleAttributes Attributes { get; }
        
        public override System.Enum Type
        {
            get => ObstacleType;
            protected set => ObstacleType = (ObstacleType)value;
        }

        public void ApplyObstacleData(ObstacleDataSO obstacleData)
        {
            var obstacleWidthHeight = obstacleData.ObstacleWidthHeight;
            SpriteRenderer.size = new Vector2(obstacleWidthHeight.x, obstacleWidthHeight.y);
            BoxCollider.size = new Vector2(obstacleWidthHeight.x, obstacleWidthHeight.y);
        }

        public override string ToString()
        {
            return $"Column{Coordinate.x},Row{Coordinate.y},Type:{ObstacleType}";
        }
    }
}