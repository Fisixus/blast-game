using AYellowpaper.SerializedCollections;
using Core.GridElements.Enums;
using UnityEngine;

namespace Core.GridObjectsData
{
    [CreateAssetMenu(fileName = "ItemData_00", menuName = "Grid Objects/New ItemData")]
    public class ItemDataSO : BaseGridObjectDataSO
    {
        [field: SerializeField] public SerializedDictionary<BoosterType, Sprite> HintSprites { get; private set; }
    }
}
