using System;
using System.Collections.Generic;
using Core.Factories.Interface;
using Core.GridElements.Enums;
using Core.GridElements.GridPawns;
using Core.Helpers.GridHelpers;
using MVP.Presenters.Handlers.Effects;
using MVP.Presenters.Handlers.Strategies.Interface;

namespace MVP.Presenters.Handlers.Strategies.Boosters
{
    public class BombStrategy : IBoosterComboStrategy
    {
        private readonly IBoosterFactory _boosterFactory;
        private readonly BombEffectHandler _bombEffectHandler;
        public Enum Type => BoosterType.Bomb;


        public BombStrategy(IBoosterFactory boosterFactory, BombEffectHandler bombEffectHandler)
        {
            _boosterFactory = boosterFactory;
            _bombEffectHandler = bombEffectHandler;
        }

        public List<BaseGridObject> FindAffectedItems(BaseGridObject[,] grid, BaseGridObject booster)
        {
            var affectedObjects = GridItemFinderHelper.FindItemsInRadiusRange(grid, booster.Coordinate, 2);
            return affectedObjects;
        }

        public void PlayExplosionEffect(BaseGridObject booster)
        {
            _bombEffectHandler.PlayBombParticle(booster, 1f);
        }

        public float GetWaitTime()
        {
            BoosterType boosterType = (BoosterType)Type;
            return _boosterFactory.BoosterDataDict[boosterType].WaitingTimeForEachDestruction;
        }

        private (int, int) GetMinMaxItemRequirement()
        {
            BoosterType boosterType = (BoosterType)Type;
            return (_boosterFactory.BoosterDataDict[boosterType].MinItemsNeeded
                , _boosterFactory.BoosterDataDict[boosterType].MaxItemsNeeded);
        }

        public bool CanCreateBooster(int itemCount)
        {
            (int min, int max) = GetMinMaxItemRequirement();
            return itemCount >= min && itemCount <= max;
        }
    }
}
