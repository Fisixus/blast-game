using AYellowpaper.SerializedCollections;
using Core.GridElements.Data.Effects;
using Core.GridElements.Effects;
using Core.GridElements.Enums;

namespace Core.Factories.Interface
{
    public interface IObstacleBlastEffectFactory : IFactory<BlastParticle>
    {
        public SerializedDictionary<ObstacleType, BlastEffectDataSO> BlastEffectDataDict { get; }
    }
}