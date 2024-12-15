using System;
using System.Collections.Generic;
using Core.Factories.Interface;
using Core.GridElements.Enums;
using Core.GridElements.GridPawns;
using Core.GridElements.GridPawns.Combo;
using UnityEngine;

namespace MVP.Presenters.Handlers
{
    public class GridObjectFactoryHandler
    {
        private readonly IItemFactory _itemFactory;
        private readonly IObstacleFactory _obstacleFactory;
        private readonly IBoosterFactory _boosterFactory;
        private readonly IComboFactory _comboFactory;

        public GridObjectFactoryHandler(IItemFactory itemFactory, IObstacleFactory obstacleFactory, 
            IBoosterFactory boosterFactory, IComboFactory comboFactory)
        {
            _itemFactory = itemFactory;
            _obstacleFactory = obstacleFactory;
            _boosterFactory = boosterFactory;
            _comboFactory = comboFactory;
        }

        public void PopulateGridWithObjects(Enum[,] gridObjectTypes, List<BaseGridObject> gridObjects)
        {
            //var isStationaryObjectExist = false;
            
            // Process the grid with column-to-row traversal
            for (int i = 0; i < gridObjectTypes.GetLength(1); i++) // Columns
            {
                ///isStationaryObjectExist = false;
                for (int j = 0; j < gridObjectTypes.GetLength(0); j++) // Rows
                {
                    Vector2Int coordinate = new Vector2Int(i, j);
                    var gridType = gridObjectTypes[j, i];

                    //if (isStationaryObjectExist)
                        //gridType = ItemType.None;
                    // Encapsulated factory logic
                    var gridObject = CreateGridObject(gridType, coordinate);
                    if (gridObject != null)
                    {
                        //if (gridObject.IsStationary)
                            //isStationaryObjectExist = true;
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

        public void DestroyOldObjects(List<BaseGridObject> emptyItems)
        {
            foreach (var emptyItem in emptyItems)
            {
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
        }
        
        public List<BaseGridObject> CreateNewItems(List<BaseGridObject> emptyItems)
        {
            var newItems = new List<BaseGridObject>();

            foreach (var emptyItem in emptyItems)
            {
                newItems.Add(_itemFactory.GenerateRandItem(emptyItem.Coordinate));
            }

            return newItems;
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

        public void DestroyCombo(Combo combo)
        {
            _comboFactory.DestroyObj(combo);
        }
        
        public Booster CreateBoosterAndDestroyOldItem(Item clickedItem, BoosterType boosterType)
        {
            var booster = _boosterFactory.GenerateBooster(boosterType, clickedItem.Coordinate, true);
            _itemFactory.DestroyObj(clickedItem);
            return booster;
        }
        
        public Combo CreateComboAndDestroyOldBooster(Booster clickedBooster, ComboType comboType)
        {
            var combo = _comboFactory.GenerateCombo(comboType, clickedBooster.Coordinate);
            _boosterFactory.DestroyObj(clickedBooster);
            return combo;
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
