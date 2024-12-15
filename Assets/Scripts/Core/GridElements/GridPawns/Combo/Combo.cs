using System;
using Core.GridElements.Data;
using Core.GridElements.Enums;
using UnityEngine;

namespace Core.GridElements.GridPawns.Combo
{
    public class Combo : BaseGridObject
    {
        [field: SerializeField] public ComboType ComboType { get; set; }

        public override Enum Type
        {
            get => ComboType;
            protected set => ComboType = (ComboType)value;
        }

        public override void ApplyData(BaseGridObjectDataSO data)
        {
            base.ApplyData(data);
            var comboData = data as ComboDataSO;
            if (comboData is null)
            {
                throw new InvalidOperationException("Invalid data type provided!");
            }
        }

        public override string ToString()
        {
            return $"Column{Coordinate.x},Row{Coordinate.y},Type:{ComboType}";
        }
    }
}