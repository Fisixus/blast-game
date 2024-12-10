using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Core.Factories.Interface;
using Core.Factories.Pools;
using Core.GridElements.Enums;
using Core.GridElements.GridPawns;
using Core.GridObjectsData;
using UnityEngine;

namespace Core.Factories
{
    public class ObstacleFactory : ObjectFactory<Obstacle>, IObstacleFactory
    {
        [field: SerializeField]
        [SerializedDictionary("Obstacle Type", "Obstacle Data")]
        public SerializedDictionary<ObstacleType, ObstacleDataSO> ObstacleDataDict { get; private set; }

        private List<Obstacle> _allObstacles;

        public override void PreInitialize()
        {
            Pool = new ObjectPool<Obstacle>(ObjPrefab, ParentTr, 32);
            _allObstacles = new List<Obstacle>(32);
        }

        public Obstacle GenerateObstacle(ObstacleType obstacleType, Vector2Int obstacleCoordinate)
        {
            var obstacle = CreateObj();
            obstacle.SetAttributes(obstacleCoordinate, obstacleType);
            obstacle.ApplyObstacleData(ObstacleDataDict[obstacleType]);
            return obstacle;
        }

        public override Obstacle CreateObj()
        {
            var item = base.CreateObj();
            _allObstacles.Add(item);
            return item;
        }

        public override void DestroyObj(Obstacle emptyItem)
        {
            base.DestroyObj(emptyItem);
            emptyItem.SetAttributes(-Vector2Int.one, ItemType.None);
            _allObstacles.Remove(emptyItem);
        }

        public void DestroyAllObstacles()
        {
            var obstaclesToDestroy = new List<Obstacle>(_allObstacles);
            base.DestroyObjs(obstaclesToDestroy);
            _allObstacles.Clear();
        }


    }
}
