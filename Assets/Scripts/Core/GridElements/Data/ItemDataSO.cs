using AYellowpaper.SerializedCollections;
using Core.GridElements.Enums;
using UnityEngine;

namespace Core.GridElements.Data
{
    [CreateAssetMenu(fileName = "ItemData_00", menuName = "Grid Objects/New ItemData")]
    public class ItemDataSO : BaseGridObjectDataSO
    {
        [field: SerializeField] public SerializedDictionary<BoosterType, Sprite> HintSprites { get; private set; }
    }
}