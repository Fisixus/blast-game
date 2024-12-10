using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Core.Enum;
using Core.GridElements.GridPawns;
using Core.GridObjectsData;
using UnityEngine;

namespace Core.Factories.Interface
{
    public interface IObstacleFactory: IFactory<Obstacle>
    {
        public SerializedDictionary<ObstacleType, ObstacleDataSO> ObstacleDataDict { get; }
        public Obstacle GenerateObstacle(ObstacleType obstacleType, Vector2Int obstacleCoordinate);

        public void DestroyAllObstacles();
    }
}
