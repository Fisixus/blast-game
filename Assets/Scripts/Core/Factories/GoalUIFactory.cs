using AYellowpaper.SerializedCollections;
using Core.Factories.Interface;
using Core.Factories.Pools;
using Core.GridElements.Data;
using Core.GridElements.Enums;
using Core.GridElements.UI;
using UnityEngine;

namespace Core.Factories
{
    public class GoalUIFactory : ObjectFactory<GoalUI>, IGoalUIFactory
    {
        [field: SerializeField]
        [SerializedDictionary("Obstacle Type", "GoalUI Data")]
        public SerializedDictionary<ObstacleType, GoalUIDataSO> GoalUIDataDict { get; private set; }

        public override void PreInitialize()
        {
            Pool = new ObjectPool<GoalUI>(ObjPrefab, ParentTr, 2);
        }
    }
}