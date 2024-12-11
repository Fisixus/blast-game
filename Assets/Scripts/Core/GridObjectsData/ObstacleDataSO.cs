using System;
using AYellowpaper.SerializedCollections;
using Core.GridElements.GridPawns;
using UnityEngine;

namespace Core.GridObjectsData
{
    [CreateAssetMenu(fileName = "ObstacleData_00", menuName = "Grid Objects/New ObstacleData")]
    public class ObstacleDataSO : BaseGridObjectDataSO
    {
        [field: SerializeField]
        public SerializedDictionary<int, Sprite>  ObstacleSpritesPerLife { get; private set; }

        [field: SerializeField]
        public ObstacleAttributes Attributes { get; private set; }
        
        [Serializable]
        public class ObstacleAttributes
        {
            [field: SerializeField]public bool IsStationary { get; set; }
            [field: SerializeField]public int Life { get; set; }
        }
    }
}
