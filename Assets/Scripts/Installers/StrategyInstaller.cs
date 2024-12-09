using DI;
using MVP.Presenters.Handlers.Strategies.Boosters;
using MVP.Presenters.Handlers.Strategies.Interface;
using MVP.Presenters.Handlers.Strategies.Matches;

namespace Installers
{
    public class StrategyInstaller : Installer
    {
        protected override void InstallBindings()
        {
            // Bind each MatchStrategy implementation
            Container.BindAsSingle<IMatchStrategy>(()=> new ItemMatchStrategy());
            // Bind each BoosterStrategy implementation
            Container.BindAsSingle<IBoosterStrategy>(() => Container.Construct<BombStrategy>());
        }
    }
}
