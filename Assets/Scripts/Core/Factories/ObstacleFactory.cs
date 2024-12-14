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


        public override void PreInitialize()
        {
            Pool = new ObjectPool<Obstacle>(ObjPrefab, ParentTr, 16);
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
            var item = base.CreateObj();
            return item;
        }

        public override void DestroyObj(Obstacle emptyItem)
        {
            base.DestroyObj(emptyItem);
            emptyItem.SetAttributes(-Vector2Int.one, ItemType.None);
        }
        


    }
}
