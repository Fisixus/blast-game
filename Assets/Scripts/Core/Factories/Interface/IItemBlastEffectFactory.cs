using AYellowpaper.SerializedCollections;
using Core.GridElements.Effects;
using Core.GridElements.Enums;
using Core.GridObjectsData.Effects;

namespace Core.Factories.Interface
{
    public interface IItemBlastEffectFactory: IFactory<BlastParticle>
    {
        public SerializedDictionary<ItemType, BlastEffectDataSO> BlastEffectDataDict { get;}


    }
}
