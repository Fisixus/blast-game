using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Core.GridElements.Data
{
    [CreateAssetMenu(fileName = "ObstacleData_00", menuName = "Grid Objects/New ObstacleData")]
    public class ObstacleDataSO : BaseGridObjectDataSO
    {
        [field: SerializeField] public SerializedDictionary<int, Sprite> ObstacleSpritesPerLife { get; private set; }

        [field: SerializeField] public int Life { get; private set; }

        // [Serializable]
        // public class ObstacleAttributes
        // {
        //     [field: SerializeField]public bool IsStationary { get; set; }
        //     [field: SerializeField]public int Life { get; set; }
        // }
    }
}