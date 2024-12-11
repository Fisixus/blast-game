using Core.Factories.Interface;
using Core.Factories.Pools;
using Core.GridElements.Effects;

namespace Core.Factories
{
    public class BombEffectFactory : ObjectFactory<BombParticle>, IBombEffectFactory
    {
        //[field: SerializeField]
        //[SerializedDictionary("Bomb Type", "Size")]
        //public SerializedDictionary<BombType, float> ParticleSizeDict { get; private set; }

        public override void PreInitialize()
        {
            Pool = new ObjectPool<BombParticle>(ObjPrefab, ParentTr, 4);
        }
    }
}
