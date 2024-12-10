using AYellowpaper.SerializedCollections;
using Core.Enum;
using Core.GridElements.Effects;
using Core.GridObjectsData.Effects;

namespace Core.Factories.Interface
{
    public interface IItemBlastEffectFactory: IFactory<BlastParticle>
    {
        public SerializedDictionary<ItemType, BlastEffectDataSO> BlastEffectDataDict { get;}


    }
}
