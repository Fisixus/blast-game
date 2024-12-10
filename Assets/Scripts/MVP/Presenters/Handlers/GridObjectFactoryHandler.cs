using System;
using System.Collections.Generic;
using Core.Enum;
using Core.Factories.Interface;
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
