using System.Collections.Generic;
using Core.Factories.Interface;
using Core.GridElements.Enums;
using Core.GridElements.GridPawns;
using Core.Helpers.GridHelpers;
using MVP.Presenters.Handlers.Effects;
using MVP.Presenters.Handlers.Strategies.Interface;

namespace MVP.Presenters.Handlers.Strategies.Boosters
{
    public class BombStrategy : IBoosterStrategy
    {
        private readonly IBoosterFactory _boosterFactory;
        private readonly BombEffectHandler _bombEffectHandler;
        public BoosterType BoosterType => BoosterType.Bomb;

        public BombStrategy(IBoosterFactory boosterFactory, BombEffectHandler bombEffectHandler)
        {
            _boosterFactory = boosterFactory;
            _bombEffectHandler = bombEffectHandler;
        }

        public List<BaseGridObject> FindAffectedItems(BaseGridObject[,] grid, Booster booster)
        {
            var affectedObjects = GridItemFinderHelper.FindItemsInRadiusRange(grid, booster.Coordinate, 2);
            return affectedObjects;
        }

        public void PlayExplosionEffect(Booster booster)
        {
            _bombEffectHandler.PlayBombParticle(booster, 1f);
        }

        public float GetWaitTime()
        {
            return _boosterFactory.BoosterDataDict[BoosterType].WaitingTimeForEachDestruction;
        }

        private (int, int) GetMinMaxItemRequirement()
        {
            return (_boosterFactory.BoosterDataDict[BoosterType].MinItemsNeeded
                , _boosterFactory.BoosterDataDict[BoosterType].MaxItemsNeeded);
        }

        public bool CanCreateBooster(int itemCount)
        {
            (int min, int max) = GetMinMaxItemRequirement();
            return itemCount >= min && itemCount <= max;
        }
    }
}
