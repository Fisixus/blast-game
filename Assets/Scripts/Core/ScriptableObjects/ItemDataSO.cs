using AYellowpaper.SerializedCollections;
using Core.Enum;
using UnityEngine;

namespace Core.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ItemData_00", menuName = "Scriptable Objects/New ItemData")]
    public class ItemDataSO : ScriptableObject
    {
        [field: SerializeField] public SerializedDictionary<BoosterType, Sprite> HintSprites { get; private set; }
        [field: SerializeField] public Vector2 ItemWidthHeight { get; private set; }
    }
}
