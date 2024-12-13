using System.Collections.Generic;
using Core.GridElements.Enums;
using UnityEngine;

namespace Core.GridElements.Data
{
    [CreateAssetMenu(fileName = "ComboData_00", menuName = "Combos/New ComboData")]
    public class ComboDataSO : BaseGridObjectDataSO
    {
        [field: SerializeField]
        public Sprite ComboSprite { get; private set; }
        [field: SerializeField]
        public List<BoosterType> BoosterIngredients{ get; private set; }
        [field: SerializeField]
        public float WaitingTimeForEachDestruction{ get; private set; }
    }
}
