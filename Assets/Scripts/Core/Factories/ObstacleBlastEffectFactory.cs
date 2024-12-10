using AYellowpaper.SerializedCollections;
using Core.Factories.Interface;
using Core.Factories.Pools;
using Core.GridElements.Effects;
using Core.GridElements.Enums;
using Core.GridObjectsData.Effects;
using UnityEngine;

namespace Core.Factories
{
    public class ObstacleBlastEffectFactory : ObjectFactory<BlastParticle>, IObstacleBlastEffectFactory
    {
        [field: SerializeField]
        [SerializedDictionary("Obstacle Type", "Blast Effect Data")]
        public SerializedDictionary<ObstacleType, BlastEffectDataSO> BlastEffectDataDict { get; private set; }

        public override void PreInitialize()
        {
            Pool = new ObjectPool<BlastParticle>(ObjPrefab, ParentTr, 8);
        }
    }
}