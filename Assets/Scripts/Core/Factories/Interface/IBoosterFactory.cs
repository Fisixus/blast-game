using AYellowpaper.SerializedCollections;
using Core.Enum;
using Core.GridElements.GridPawns;
using Core.ScriptableObjects;
using UnityEngine;

namespace Core.Factories.Interface
{
    public interface IBoosterFactory
    {
        public SerializedDictionary<BoosterType, BoosterDataSO> BoosterDataDict { get; }
        public Booster GenerateBooster(BoosterType boosterType, Vector2Int coord); // Specialized method
        public void DestroyAllBoosters();
        
    }
}
