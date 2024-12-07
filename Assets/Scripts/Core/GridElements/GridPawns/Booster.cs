using Core.Enum;
using Core.ScriptableObjects;
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

        public void ApplyBoosterData(BoosterDataSO boosterData)
        {
            SpriteRenderer.sprite = boosterData.ItemSprite;
            var itemWidthHeight = boosterData.ItemWidthHeight;
            SpriteRenderer.size = new Vector2(itemWidthHeight.x, itemWidthHeight.y);
            BoxCollider.size = new Vector2(itemWidthHeight.x, itemWidthHeight.y);
        }


        public override string ToString()
        {
            return $"Column{Coordinate.x},Row{Coordinate.y},Type:{BoosterType}";
        }
    }
}
