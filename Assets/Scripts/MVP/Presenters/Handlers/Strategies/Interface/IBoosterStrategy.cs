using System;
using System.Collections.Generic;
using Core.GridElements.GridPawns;

namespace MVP.Presenters.Handlers.Strategies.Interface
{
    public interface IBoosterStrategy
    {
        public Enum Type { get; }
        public List<BaseGridObject> FindAffectedItems(BaseGridObject[,] grid, BaseGridObject booster);
        public void PlayExplosionEffect(BaseGridObject booster);
        public float GetWaitTime();
        public bool CanCreateBooster(int itemCount);
    }
}
