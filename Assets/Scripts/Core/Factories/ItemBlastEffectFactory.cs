using AYellowpaper.SerializedCollections;
using Core.Factories.Interface;
using Core.Factories.Pools;
using Core.GridElements.Effects;
using Core.GridElements.Enums;
using Core.GridObjectsData.Effects;
using UnityEngine;

namespace Core.Factories
{
    public class ItemBlastEffectFactory : ObjectFactory<BlastParticle>, IItemBlastEffectFactory
    {
        [field: SerializeField]
        [SerializedDictionary("Item Type", "Blast Effect Data")]
        public SerializedDictionary<ItemType, BlastEffectDataSO> BlastEffectDataDict { get; private set; }

        public override void PreInitialize()
        {
            Pool = new ObjectPool<BlastParticle>(ObjPrefab, ParentTr, 16);
        }
    }
}
