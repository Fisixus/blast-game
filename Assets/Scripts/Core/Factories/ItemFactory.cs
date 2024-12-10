using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Core.Enum;
using Core.Factories.Interface;
using Core.Factories.Pools;
using Core.GridElements.GridPawns;
using Core.ScriptableObjects;
using UnityEngine;

namespace Core.Factories
{
    public class ItemFactory : ObjectFactory<Item>, IItemFactory
    {
        [field: SerializeField]
        public ItemSpawnProbabilityDistributionSO Probability { get; private set; }
        [field: SerializeField]
        [SerializedDictionary("Item Type", "Item Data")]
        public SerializedDictionary<ItemType, ItemDataSO> ItemDataDict { get; private set; }

        private List<Item> _allItems;

        public override void PreInitialize()
        {
            Pool = new ObjectPool<Item>(ObjPrefab, ParentTr, 64);
            _allItems = new List<Item>(64);
        }
        
        public Item GenerateRandItem(Vector2Int itemCoordinate)
        {
            var itemType = Probability.PickRandomItemType();
            var item = CreateObj();
            item.SetAttributes(itemCoordinate, itemType);
            item.ApplyItemData(ItemDataDict[itemType]);
            return item;
        }

        public Item GenerateItem(ItemType itemType, Vector2Int itemCoordinate)
        {
            var item = CreateObj();
            item.SetAttributes(itemCoordinate, itemType);
            item.ApplyItemData(ItemDataDict[itemType]);
            return item;
        }

        public override Item CreateObj()
        {
            var item = base.CreateObj();
            _allItems.Add(item);
            return item;
        }

        public override void DestroyObj(Item emptyItem)
        {
            base.DestroyObj(emptyItem);
            emptyItem.SetAttributes(-Vector2Int.one, ItemType.None);
            _allItems.Remove(emptyItem);
        }

        public void DestroyAllItems()
        {
            var itemsToDestroy = new List<Item>(_allItems);
            base.DestroyObjs(itemsToDestroy);
            _allItems.Clear();
        }


    }
}
