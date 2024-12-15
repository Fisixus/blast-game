using AYellowpaper.SerializedCollections;
using Core.GridElements.Data.Effects;
using Core.GridElements.Effects;
using Core.GridElements.Enums;

namespace Core.Factories.Interface
{
    public interface IItemBlastEffectFactory : IFactory<BlastParticle>
    {
        public SerializedDictionary<ItemType, BlastEffectDataSO> BlastEffectDataDict { get; }
    }
}