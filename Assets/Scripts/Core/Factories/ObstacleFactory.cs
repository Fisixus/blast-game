using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Core.Factories.Interface;
using Core.Factories.Pools;
using Core.GridElements.Data;
using Core.GridElements.Enums;
using Core.GridElements.GridPawns;
using UnityEngine;

namespace Core.Factories
{
    public class ObstacleFactory : ObjectFactory<Obstacle>, IObstacleFactory
    {
        [field: SerializeField]
        [SerializedDictionary("Obstacle Type", "Obstacle Data")]
        public SerializedDictionary<ObstacleType, ObstacleDataSO> ObstacleDataDict { get; private set; }
        private List<Obstacle> _allObstacles = new();


        public override void PreInitialize()
        {
            Pool = new ObjectPool<Obstacle>(ObjPrefab, ParentTr, 16);
            _allObstacles = new List<Obstacle>(16);

        }

        public Obstacle GenerateObstacle(ObstacleType obstacleType, Vector2Int obstacleCoordinate)
        {
            var obstacle = CreateObj();
            obstacle.SetAttributes(obstacleCoordinate, obstacleType);
            obstacle.ApplyData(ObstacleDataDict[obstacleType]);
            return obstacle;
        }

        public override Obstacle CreateObj()
        {
            var obstacle = base.CreateObj();
            _allObstacles.Add(obstacle);
            return obstacle;
        }

        public override void DestroyObj(Obstacle emptyObstacle)
        {
            base.DestroyObj(emptyObstacle);
            emptyObstacle.SetAttributes(-Vector2Int.one, ItemType.None);
            _allObstacles.Remove(emptyObstacle);

        }
        
        public void DestroyAllObstacles()
        {
            var obstaclesToDestroy = new List<Obstacle>(_allObstacles);
            base.DestroyObjs(obstaclesToDestroy);
            _allObstacles.Clear();
        }

    }
}
