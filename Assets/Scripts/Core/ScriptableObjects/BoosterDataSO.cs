
using UnityEngine;

namespace Core.ScriptableObjects
{
    [CreateAssetMenu(fileName = "BoosterData_00", menuName = "Scriptable Objects/New BoosterData")]
    public class BoosterDataSO : ScriptableObject
    {
        [field: SerializeField]
        public Sprite ItemSprite { get; private set; }
        [field: SerializeField]
        public Vector2 ItemWidthHeight { get; private set; }
        //[field: SerializeField]
        //public List<BoosterType> BoosterParents{ get; private set; }
        [field: SerializeField]
        public int MinItemsNeeded{ get; private set; }
        [field: SerializeField]
        public int MaxItemsNeeded{ get; private set; }
        [field: SerializeField]
        public float WaitingTimeForEachItemDestroyed{ get; private set; }
    }
}
