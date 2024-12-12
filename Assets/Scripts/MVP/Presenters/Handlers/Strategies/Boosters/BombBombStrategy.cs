using System.Collections.Generic;
using Core.Factories.Interface;
using Core.GridElements.Enums;
using Core.GridElements.GridPawns;
using Core.Helpers.GridHelpers;
using MVP.Presenters.Handlers.Effects;
using MVP.Presenters.Handlers.Strategies.Interface;

namespace MVP.Presenters.Handlers.Strategies.Boosters
{
    public class BombBombStrategy : IBoosterStrategy
    {
        private readonly IComboFactory _comboFactory;
        private readonly BombEffectHandler _bombEffectHandler;
        public BoosterType BoosterType => BoosterType.BombBomb;

        public BombBombStrategy(IComboFactory comboFactory, BombEffectHandler bombEffectHandler)
        {
            _comboFactory = comboFactory;
            _bombEffectHandler = bombEffectHandler;
        }

        public List<BaseGridObject> FindAffectedItems(BaseGridObject[,] grid, Booster booster)
        {
            var affectedObjects = GridItemFinderHelper.FindItemsInRadiusRange(grid, booster.Coordinate, 3);
            return affectedObjects;
        }

        public void PlayExplosionEffect(Booster booster)
        {
            _bombEffectHandler.PlayBombParticle(booster, 1.5f);
        }

        public float GetWaitTime()
        {
            return _comboFactory.ComboDataDict[BoosterType].WaitingTimeForEachDestruction;
        }

        public bool CanCreateBooster(int itemCount)
        {
            return false;
        }
        
    }
}