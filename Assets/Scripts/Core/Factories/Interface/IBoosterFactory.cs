using AYellowpaper.SerializedCollections;
using Core.GridElements.Data;
using Core.GridElements.Enums;
using Core.GridElements.GridPawns;
using UnityEngine;

namespace Core.Factories.Interface
{
    public interface IBoosterFactory: IFactory<Booster>
    {
        public SerializedDictionary<BoosterType, BoosterDataSO> BoosterDataDict { get; }
        public Booster GenerateBooster(BoosterType boosterType, Vector2Int coord, bool isEffectOn=false);
        
    }
}
