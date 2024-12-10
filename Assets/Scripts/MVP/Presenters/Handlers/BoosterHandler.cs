
using System.Collections.Generic;
using System.Linq;
using Core.GridElements.Enums;
using Core.GridElements.GridPawns;
using MVP.Presenters.Handlers.Strategies.Interface;
using UnityEngine;

namespace MVP.Presenters.Handlers
{
    public class BoosterHandler
    {
        private BaseGridObject[,] _grid;
        private readonly Dictionary<BoosterType, IBoosterStrategy> _boosterStrategies = new();
        
        public BoosterHandler(IEnumerable<IBoosterStrategy> boosterStrategies)
        {
            // Register strategies
            foreach (var strategy in boosterStrategies)
            {
                _boosterStrategies[strategy.BoosterType] = strategy;
            }
        }
        
        public void Initialize(BaseGridObject[,] grid)
        {
            _grid = grid;
        }
        
        public BoosterType IsBoosterCreatable(List<BaseGridObject> matchedObjs)
        {
            foreach (var strategy in _boosterStrategies.Values.Where(strategy =>
                         strategy.CanCreateBooster(matchedObjs.Count)))
            {
                // Handle randomness for horizontal/vertical rockets
                if (strategy.BoosterType is BoosterType.RocketHorizontal or BoosterType.RocketVertical)
                {
                    return (BoosterType)Random.Range((int)BoosterType.RocketHorizontal,
                        (int)BoosterType.RocketVertical + 1);
                }

                return strategy.BoosterType;
            }

            return BoosterType.None;
        }
    }
}
