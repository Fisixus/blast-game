using AYellowpaper.SerializedCollections;
using Core.Enum;
using Core.GridElements.Effects;
using Core.GridObjectsData.Effects;

namespace Core.Factories.Interface
{
    public interface IObstacleBlastEffectFactory: IFactory<BlastParticle>
    {
        public SerializedDictionary<ObstacleType, BlastEffectDataSO> BlastEffectDataDict { get;}


    }
}
