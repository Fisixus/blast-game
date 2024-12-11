using System;
using System.Collections.Generic;
using Core.GridElements.Enums;
using Core.GridObjectsData;
using UnityEngine;

namespace Core.GridElements.GridPawns
{
    public class Item : BaseGridObject
    {
        [field: SerializeField] public ItemType ItemType { get; set; }
        public BoosterType HintType { get; private set; }
        public override System.Enum Type
        {
            get => ItemType;
            protected set => ItemType = (ItemType)value;
        }

        private Dictionary<BoosterType, Sprite> _hintSprites = new();
        

        public void ApplyHintSprite(BoosterType hintType)
        {
            HintType = hintType;
            SpriteRenderer.sprite = _hintSprites[hintType];
        }
        
        public override void ApplyData(BaseGridObjectDataSO data)
        {
            base.ApplyData(data);
            var itemData = data as ItemDataSO;
            if(itemData is null)
            {
                throw new InvalidOperationException("Invalid data type provided!");
            }
            
            _hintSprites = itemData.HintSprites;
            ApplyHintSprite(BoosterType.None);
            
        }

        public override string ToString()
        {
            return $"Column{Coordinate.x},Row{Coordinate.y},Type:{ItemType}";
        }
    }
}
