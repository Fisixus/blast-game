using System;
using System.Collections.Generic;
using Core.GridElements.Data;
using Core.GridElements.Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.GridElements.GridPawns
{
    public class Obstacle : BaseGridObject
    {
        [field: SerializeField] public ObstacleType ObstacleType { get; set; }
        public int Life { get; private set; }
        private Dictionary<int, Sprite> ObstacleSpritesPerLife { get; set; }

        public override Enum Type
        {
            get => ObstacleType;
            protected set => ObstacleType = (ObstacleType)value;
        }

        public int TakeDamage()
        {
            Life--;
            if (Life <= 0)
                return 0;
            BaseGridObjectEffect.Shake(Random.Range(0.1f, 0.2f));
            SpriteRenderer.sprite = ObstacleSpritesPerLife[Life];
            return Life;
        }

        public override void ApplyData(BaseGridObjectDataSO data)
        {
            base.ApplyData(data);

            var obstacleData = data as ObstacleDataSO;
            if (obstacleData is null)
            {
                throw new InvalidOperationException("Invalid data type provided!");
            }

            Life = obstacleData.Life;
            ObstacleSpritesPerLife = obstacleData.ObstacleSpritesPerLife;
            SpriteRenderer.sprite = ObstacleSpritesPerLife[Life];
        }

        public override string ToString()
        {
            return $"Column{Coordinate.x},Row{Coordinate.y},Type:{ObstacleType}";
        }
    }
}