using UnityEngine;

namespace Core.GridElements.Data
{
    [CreateAssetMenu(fileName = "BoosterData_00", menuName = "Grid Objects/New BoosterData")]
    public class BoosterDataSO : BaseGridObjectDataSO
    {
        [field: SerializeField] public Sprite BoosterSprite { get; private set; }

        [field: SerializeField] public int MinItemsNeeded { get; private set; }
        [field: SerializeField] public int MaxItemsNeeded { get; private set; }
        [field: SerializeField] public float WaitingTimeForEachDestruction { get; private set; }
    }
}