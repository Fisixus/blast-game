using Core.ScriptableObjects;

namespace Core.Factories.Interface
{
    public interface IItemFactory
    {
        public ItemSpawnProbabilityDistributionSO Probability { get; }
        
        //public SerializedDictionary<ItemType, ItemData> ItemDataDict { get; }
        
        //public List<BaseGridObject> GenerateItems(ItemType[,] itemTypes);
        //public BaseGridObject GenerateItem(Vector2Int itemCoordinate);
        public void DestroyAllItems();
    }
}
