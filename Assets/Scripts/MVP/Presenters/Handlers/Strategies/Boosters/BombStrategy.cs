using System.Collections.Generic;
using Core.Enum;
using Core.Factories.Interface;
using Core.GridElements.GridPawns;
using Core.Helpers.GridHelpers;
using MVP.Presenters.Handlers.Strategies.Interface;

namespace MVP.Presenters.Handlers.Strategies.Boosters
{
    public class BombStrategy : IBoosterStrategy
    {
        private readonly IBoosterFactory _boosterFactory;
        public BoosterType BoosterType => BoosterType.Bomb;

        public BombStrategy(IBoosterFactory boosterFactory)
        {
            _boosterFactory = boosterFactory;
        }

        public List<BaseGridObject> FindAffectedItems(BaseGridObject[,] grid, Booster booster)
        {
            var affectedObjects = GridItemFinderHelper.FindItemsInCircleRange(grid, booster.Coordinate, 5);
            return affectedObjects;
        }

        public void PlayExplosionEffect(Booster booster)
        {
            //m_BombEffectHandler.PlayBombParticle(booster);
        }

        public float GetWaitTime()
        {
            return _boosterFactory.BoosterDataDict[BoosterType].WaitingTimeForEachDestruction;
        }

        public (int, int) GetMinMaxItemRequirement()
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
