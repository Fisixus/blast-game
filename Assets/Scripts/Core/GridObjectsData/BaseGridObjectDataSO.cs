using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.GridObjectsData
{
    public class BaseGridObjectDataSO : ScriptableObject
    {
        [field: SerializeField] public Vector2 GridObjectWidthHeight { get; private set; }
    }
}
