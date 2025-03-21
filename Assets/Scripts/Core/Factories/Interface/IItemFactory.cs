using AYellowpaper.SerializedCollections;
using Core.GridElements.Data;
using Core.GridElements.Enums;
using Core.GridElements.GridPawns;
using Core.ProbabilityData;
using UnityEngine;

namespace Core.Factories.Interface
{
    public interface IItemFactory : IFactory<Item>
    {
        public ItemSpawnProbabilityDistributionSO Probability { get; }
        public SerializedDictionary<ItemType, ItemDataSO> ItemDataDict { get; }
        public Item GenerateRandItem(Vector2Int itemCoordinate);
        public Item GenerateItem(ItemType itemType, Vector2Int itemCoordinate);

        public void DestroyAllItems();
    }
}