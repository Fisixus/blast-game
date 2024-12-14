using System;
using System.Collections.Generic;
using Core.Factories.Interface;
using Core.GridElements.Enums;
using Core.GridElements.GridPawns;
using Core.Helpers.GridHelpers;
using MVP.Presenters.Handlers.Effects;
using MVP.Presenters.Strategies.Interface;

namespace MVP.Presenters.Strategies.Boosters
{
    public class BombBombStrategy : IBoosterComboStrategy
    {
        private readonly IComboFactory _comboFactory;
        private readonly BombEffectHandler _bombEffectHandler;
        public Enum Type => ComboType.BombBomb;

        public BombBombStrategy(IComboFactory comboFactory, BombEffectHandler bombEffectHandler)
        {
            _comboFactory = comboFactory;
            _bombEffectHandler = bombEffectHandler;
        }

        public List<BaseGridObject> FindAffectedItems(BaseGridObject[,] grid, BaseGridObject booster)
        {
            var affectedObjects = GridItemFinderHelper.FindItemsInRadiusRange(grid, booster.Coordinate, 3);
            return affectedObjects;
        }

        public void PlayExplosionEffect(BaseGridObject booster)
        {
            _bombEffectHandler.PlayBombParticle(booster, 1.5f);
        }

        public float GetWaitTime()
        {
            ComboType comboType = (ComboType)Type;
            return _comboFactory.ComboDataDict[comboType].WaitingTimeForEachDestruction;
        }

        public bool CanCreateBooster(int itemCount)
        {
            return false;
        }
        
    }
}