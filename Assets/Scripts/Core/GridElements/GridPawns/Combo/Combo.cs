using System;
using Core.GridObjectsData;

namespace Core.GridElements.GridPawns.Combo
{
    public class Combo : Booster
    {
        public override void ApplyData(BaseGridObjectDataSO data)
        {
            base.ApplyData(data);
            var comboData = data as ComboDataSO;
            if(comboData is null)
            {
                throw new InvalidOperationException("Invalid data type provided!");
            }
        }
    }
}
