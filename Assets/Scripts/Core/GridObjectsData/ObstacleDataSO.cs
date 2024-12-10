using UnityEngine;

namespace Core.GridObjectsData
{
    [CreateAssetMenu(fileName = "ObstacleData_00", menuName = "Grid Objects/New ObstacleData")]
    public class ObstacleDataSO : ScriptableObject
    {
        [field: SerializeField]
        public Sprite ObstacleSprite { get; private set; }
        [field: SerializeField] 
        public Vector2 ObstacleWidthHeight { get; private set; }
    }
}
