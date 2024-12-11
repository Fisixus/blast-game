using System;
using System.Collections.Generic;
using Core.GridElements.Enums;
using Core.GridObjectsData;
using UnityEngine;

namespace Core.GridElements.GridPawns
{
    public class Obstacle : BaseGridObject
    {
        [field: SerializeField] public ObstacleType ObstacleType { get; set; }
        private ObstacleDataSO.ObstacleAttributes Attributes { get; set;}
        private Dictionary<int, Sprite>  ObstacleSpritesPerLife { get; set; }

        public override System.Enum Type
        {
            get => ObstacleType;
            protected set => ObstacleType = (ObstacleType)value;
        }
        
        public int TakeDamage()
        {
            Attributes.Life--;
            if (Attributes.Life == 0) 
                return 0;
            
            SpriteRenderer.sprite = ObstacleSpritesPerLife[Attributes.Life];
            return Attributes.Life;
        }
        public override void ApplyData(BaseGridObjectDataSO data)
        {
            var obstacleData = data as ObstacleDataSO;
            if(obstacleData is null)
            {
                throw new InvalidOperationException("Invalid data type provided!");
            }

            Attributes = obstacleData.Attributes;
            ObstacleSpritesPerLife = obstacleData.ObstacleSpritesPerLife;
            SpriteRenderer.sprite = ObstacleSpritesPerLife[Attributes.Life];
            
            var obstacleWidthHeight = obstacleData.GridObjectWidthHeight;
            SpriteRenderer.size = new Vector2(obstacleWidthHeight.x, obstacleWidthHeight.y);
            BoxCollider.size = new Vector2(obstacleWidthHeight.x, obstacleWidthHeight.y);
        }

        public override string ToString()
        {
            return $"Column{Coordinate.x},Row{Coordinate.y},Type:{ObstacleType}";
        }
    }
}