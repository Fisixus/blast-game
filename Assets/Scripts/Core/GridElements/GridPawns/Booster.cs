using System;
using Core.GridElements.Data;
using Core.GridElements.Enums;
using UnityEngine;

namespace Core.GridElements.GridPawns
{
    public class Booster : BaseGridObject
    {
        [field: SerializeField] public BoosterType BoosterType { get; set; }

        public override Enum Type
        {
            get => BoosterType;
            protected set => BoosterType = (BoosterType)value;
        }

        public override void ApplyData(BaseGridObjectDataSO data)
        {
            base.ApplyData(data);
            var boosterData = data as BoosterDataSO;
            if (boosterData is null)
            {
                throw new InvalidOperationException("Invalid data type provided!");
            }
        }

        public override string ToString()
        {
            return $"Column{Coordinate.x},Row{Coordinate.y},Type:{BoosterType}";
        }
    }
}