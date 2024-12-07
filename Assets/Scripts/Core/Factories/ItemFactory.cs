using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Core.Enum;
using Core.Factories.Interface;
using Core.GridElements.GridPawns;
using Core.Pools;
using Core.ScriptableObjects;
using UnityEngine;

namespace Core.Factories
{
    public class ItemFactory : ObjectFactory<Item>, IItemFactory
    {
        [field: SerializeField]
        [SerializedDictionary("Item Type", "Item Data")]
        public SerializedDictionary<ItemType, ItemDataSO> ItemDataDict { get; private set; }
        public ItemSpawnProbabilityDistributionSO Probability { get; private set; }

        private List<Item> _allItems;

        public void Awake()
        {
            SetPool(64);
            _allItems = new List<Item>(64);
        }

        public BaseGridObject GenerateItem(Vector2Int itemCoordinate)
        {
            var itemType = Probability.PickRandomItemType();
            var item = CreateObj();
            item.SetAttributes(itemCoordinate, itemType, false);
            item.ApplyItemData(ItemDataDict[itemType]);
            return item;
        }

        public List<Item> GenerateItems(System.Enum[,] itemTypes)
        {
            for (var i = 0; i < itemTypes.GetLength(1); i++)
            for (var j = 0; j < itemTypes.GetLength(0); j++)
            {
                if (itemTypes[j, i] is not ItemType) continue;
                var itemType = (ItemType)itemTypes[j, i];
                
                var item = CreateObj();
                item.SetAttributes(new Vector2Int(i, j), itemType, false);
                item.ApplyItemData(ItemDataDict[itemType]);
            }

            return _allItems;
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
            emptyItem.SetAttributes(-Vector2Int.one, ItemType.None, true);
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
