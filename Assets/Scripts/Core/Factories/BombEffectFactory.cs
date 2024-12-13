using Core.Factories.Interface;
using Core.Factories.Pools;
using Core.GridElements.Effects;

namespace Core.Factories
{
    public class BombEffectFactory : ObjectFactory<BombParticle>, IBombEffectFactory
    {
        public override void PreInitialize()
        {
            Pool = new ObjectPool<BombParticle>(ObjPrefab, ParentTr, 4);
        }
    }
}
