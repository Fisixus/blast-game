using UnityEngine;

namespace Core.GridElements.Data
{
    [CreateAssetMenu(fileName = "GoalUIData_00", menuName = "Goal/New GoalUIData")]
    public class GoalUIDataSO : ScriptableObject
    {
        [field: SerializeField] public Texture ObstacleTexture { get; private set; }
        [field: SerializeField] public Vector2 ObstacleWidthHeight { get; private set; }
    }
}
