using UnityEngine;

namespace Core.GridElements.Data
{
    public class BaseGridObjectDataSO : ScriptableObject
    {
        [field: SerializeField] public Vector2 GridObjectWidthHeight { get; private set; }
        [field: SerializeField] public bool IsStationary { get; private set; }
    }
}