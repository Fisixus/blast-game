using System;
using System.Collections.Generic;
using Core.Factories.Interface;
using Core.GridElements.Enums;
using Core.GridElements.GridPawns;
using UnityEngine;

namespace MVP.Presenters.Handlers
{
    public class GridObjectFactoryHandler
    {
        private readonly IItemFactory _itemFactory;
        private readonly IObstacleFactory _obstacleFactory;
        private readonly IBoosterFactory _boosterFactory;

        public GridObjectFactoryHandler(IItemFactory itemFactory, IObstacleFactory obstacleFactory, IBoosterFactory boosterFactory)
        {
            _itemFactory = itemFactory;
            _obstacleFactory = obstacleFactory;
            _boosterFactory = boosterFactory;
        }

        public void Initialize(Enum[,] gridObjectTypes, List<BaseGridObject> gridObjects)
        {
            // Process the grid with column-to-row traversal
            for (int i = 0; i < gridObjectTypes.GetLength(1); i++) // Columns
            {
                for (int j = 0; j < gridObjectTypes.GetLength(0); j++) // Rows
                {
                    Vector2Int coordinate = new Vector2Int(i, j);
                    var gridType = gridObjectTypes[j, i];

                    // Encapsulated factory logic
                    var gridObject = CreateGridObject(gridType, coordinate);
                    if (gridObject != null)
                    {
                        gridObjects.Add(gridObject);
                    }
                }
            }
        }

        public void DestroyAllGridObjects()
        {
            _itemFactory.DestroyAllItems();
            _boosterFactory.DestroyAllBoosters();
            _obstacleFactory.DestroyAllObstacles();
        }
        
        public List<BaseGridObject> GenerateNewItems(List<BaseGridObject> emptyItems)
        {
            var newItems = new List<BaseGridObject>();

            foreach (var emptyItem in emptyItems)
            {
                newItems.Add(_itemFactory.GenerateRandItem(emptyItem.Coordinate));
                switch (emptyItem)
                {
                    case Item item:
                        _itemFactory.DestroyObj(item);
                        break;
                    case Booster booster:
                        _boosterFactory.DestroyObj(booster);
                        break;
                    case Obstacle obstacle:
                        _obstacleFactory.DestroyObj(obstacle);
                        break;
                }
            }

            return newItems;
        }
        
        public Booster CreateBoosterAndDestroyOldItem(Item item, BoosterType boosterType)
        {
            var booster = _boosterFactory.GenerateBooster(boosterType, item.Coordinate, true);
            _itemFactory.DestroyObj(item);
            return booster;
        }
        private BaseGridObject CreateGridObject(Enum gridType, Vector2Int coordinate)
        {
            switch (gridType)
            {
                case ItemType itemType:
                    return _itemFactory.GenerateItem(itemType, coordinate);
                case ObstacleType obstacleType:
                    return _obstacleFactory.GenerateObstacle(obstacleType, coordinate);
                case BoosterType boosterType:
                    return _boosterFactory.GenerateBooster(boosterType, coordinate, false);
                default:
                    Debug.LogWarning($"Unknown grid type: {gridType} at {coordinate}");
                    return null;
            }
        }
    }
}
