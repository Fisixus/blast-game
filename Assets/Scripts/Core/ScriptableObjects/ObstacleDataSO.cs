using UnityEngine;

namespace Core.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ObstacleData_00", menuName = "Scriptable Objects/New ObstacleData")]
    public class ObstacleDataSO : ScriptableObject
    {
        [field: SerializeField] public Vector2 ObstacleWidthHeight { get; private set; }
    }
}
