using System.Collections.Generic;
using Core.GridElements.Enums;
using Core.GridElements.GridPawns;

namespace MVP.Presenters.Handlers.Strategies.Interface
{
    public interface IBoosterStrategy
    {
        public BoosterType BoosterType { get; }
        public List<BaseGridObject> FindAffectedItems(BaseGridObject[,] grid, Booster booster);
        public void PlayExplosionEffect(Booster booster);
        public float GetWaitTime();
        public bool CanCreateBooster(int itemCount);
    }
}
