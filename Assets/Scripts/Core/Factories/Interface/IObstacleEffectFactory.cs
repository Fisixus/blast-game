using AYellowpaper.SerializedCollections;
using Core.GridElements.Effects;
using Core.GridElements.Enums;
using Core.GridObjectsData.Effects;

namespace Core.Factories.Interface
{
    public interface IObstacleBlastEffectFactory: IFactory<BlastParticle>
    {
        public SerializedDictionary<ObstacleType, BlastEffectDataSO> BlastEffectDataDict { get;}


    }
}
