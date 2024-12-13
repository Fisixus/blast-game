using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Core.GridElements.Data;
using Core.GridElements.Enums;
using Core.GridElements.GridPawns;
using UnityEngine;

namespace Core.Factories.Interface
{
    public interface IObstacleFactory: IFactory<Obstacle>
    {
        public SerializedDictionary<ObstacleType, ObstacleDataSO> ObstacleDataDict { get; }
        public Obstacle GenerateObstacle(ObstacleType obstacleType, Vector2Int obstacleCoordinate);

    }
}
