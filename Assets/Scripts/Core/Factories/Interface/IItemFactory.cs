using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Core.Enum;
using Core.GridElements.GridPawns;
using Core.ScriptableObjects;
using UnityEngine;

namespace Core.Factories.Interface
{
    public interface IItemFactory
    {
        public ItemSpawnProbabilityDistributionSO Probability { get; }
        public SerializedDictionary<ItemType, ItemDataSO> ItemDataDict { get; }
        public List<Item> GenerateItems(ItemType[,] itemTypes);
        public BaseGridObject GenerateItem(Vector2Int itemCoordinate);
        public void DestroyAllItems();
    }
}
