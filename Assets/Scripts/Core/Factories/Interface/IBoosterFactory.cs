using System.Collections.Generic;
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
        public List<Booster> GenerateBoosters(System.Enum[,] boosterTypes);
        public Booster GenerateBooster(BoosterType boosterType, Vector2Int coord);
        public void DestroyAllBoosters();
        
    }
}
