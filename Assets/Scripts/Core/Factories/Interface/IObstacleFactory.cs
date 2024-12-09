using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Core.Enum;
using Core.GridElements.GridPawns;
using Core.ScriptableObjects;
using UnityEngine;

namespace Core.Factories.Interface
{
    public interface IObstacleFactory: IFactory<Obstacle>
    {
        public SerializedDictionary<ObstacleType, ObstacleDataSO> ObstacleDataDict { get; }
        public List<Obstacle> GenerateObstacles(System.Enum[,] itemTypes);
        public void DestroyAllObstacles();
    }
}
