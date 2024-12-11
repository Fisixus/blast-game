using AYellowpaper.SerializedCollections;
using Core.GridElements.Effects;

namespace Core.Factories.Interface
{
    public interface IBombEffectFactory : IFactory<BombParticle>
    {
        //public SerializedDictionary<BombType, float> ParticleSizeDict { get; }
    }
}