using System;
using Core.GridElements.Enums;
using Core.GridObjectsData;
using UnityEngine;

namespace Core.GridElements.GridPawns
{
    public class Booster : BaseGridObject
    {
        [field: SerializeField] public BoosterType BoosterType { get; set; }

        public override System.Enum Type
        {
            get => BoosterType;
            protected set => BoosterType = (BoosterType)value;
        }
        public override void ApplyData(BaseGridObjectDataSO data)
        {
            var boosterData = data as BoosterDataSO;
            if(boosterData is null)
            {
                throw new InvalidOperationException("Invalid data type provided!");
            }
            SpriteRenderer.sprite = boosterData.BoosterSprite;
            var boosterWidthHeight = boosterData.GridObjectWidthHeight;
            SpriteRenderer.size = new Vector2(boosterWidthHeight.x, boosterWidthHeight.y);
            BoxCollider.size = new Vector2(boosterWidthHeight.x, boosterWidthHeight.y);
        }

        public override string ToString()
        {
            return $"Column{Coordinate.x},Row{Coordinate.y},Type:{BoosterType}";
        }
    }
}
