using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Core.Enum;
using Core.GridElements.GridPawns;
using Core.GridObjectsData;
using Core.ProbabilityData;
using UnityEngine;

namespace Core.Factories.Interface
{
    public interface IItemFactory: IFactory<Item>
    {
        public ItemSpawnProbabilityDistributionSO Probability { get; }
        public SerializedDictionary<ItemType, ItemDataSO> ItemDataDict { get; }
        public Item GenerateRandItem(Vector2Int itemCoordinate);
        public Item GenerateItem(ItemType itemType, Vector2Int itemCoordinate);
        public void DestroyAllItems();
    }
}
