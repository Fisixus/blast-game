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
            Container.BindAsTransient<IMatchStrategy>(()=> new ItemMatchStrategy());
            Container.BindAsTransient<IMatchStrategy>(()=> new BoxMatchStrategy());
            Container.BindAsTransient<IMatchStrategy>(()=> new VaseMatchStrategy());
            
            // Bind each BoosterStrategy implementation
            Container.BindAsTransient<IBoosterStrategy>(() => Container.Construct<BombStrategy>());
            Container.BindAsTransient<IBoosterStrategy>(() => Container.Construct<BombBombStrategy>());
        }
    }
}
