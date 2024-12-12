using System.Collections.Generic;
using Core.GridElements.Enums;
using Core.GridObjectsData;
using UnityEngine;

namespace Core.GridElements.GridPawns.Combo
{
    [CreateAssetMenu(fileName = "ComboData_00", menuName = "Combos/New ComboData")]
    public class ComboDataSO : BaseGridObjectDataSO
    {
        [field: SerializeField]
        public Sprite ComboSprite { get; private set; }
        [field: SerializeField]
        public List<BoosterType> BoosterParents{ get; private set; }
        [field: SerializeField]
        public float WaitingTimeForEachDestruction{ get; private set; }
    }
}
