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
        
        public List<Obstacle> GenerateObstacles(System.Enum[,] itemTypes)
        {
            for (var i = 0; i < itemTypes.GetLength(1); i++)
            for (var j = 0; j < itemTypes.GetLength(0); j++)
            {
                if (itemTypes[j, i] is not ObstacleType) continue;
                var obstacleType = (ObstacleType)itemTypes[j, i];
                
                var obstacle = CreateObj();
                obstacle.SetAttributes(new Vector2Int(i, j), obstacleType);
                obstacle.ApplyObstacleData(ObstacleDataDict[obstacleType]);
            }

            return _allObstacles;
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
