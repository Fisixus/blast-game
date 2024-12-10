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

        public void ApplyItemData(ItemDataSO itemData)
        {
            _hintSprites = itemData.HintSprites;
            ApplyHintSprite(BoosterType.None);
            var itemWidthHeight = itemData.ItemWidthHeight;
            SpriteRenderer.size = new Vector2(itemWidthHeight.x, itemWidthHeight.y);
            BoxCollider.size = new Vector2(itemWidthHeight.x, itemWidthHeight.y);
        }
        public override string ToString()
        {
            return $"Column{Coordinate.x},Row{Coordinate.y},Type:{ItemType}";
        }
    }
}
