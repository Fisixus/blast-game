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

        public void ApplyBoosterData(BoosterDataSO boosterData)
        {
            SpriteRenderer.sprite = boosterData.BoosterSprite;
            var itemWidthHeight = boosterData.BoosterWidthHeight;
            SpriteRenderer.size = new Vector2(itemWidthHeight.x, itemWidthHeight.y);
            BoxCollider.size = new Vector2(itemWidthHeight.x, itemWidthHeight.y);
        }


        public override string ToString()
        {
            return $"Column{Coordinate.x},Row{Coordinate.y},Type:{BoosterType}";
        }
    }
}
