using DI;
using MVP.Presenters.Strategies.Boosters;
using MVP.Presenters.Strategies.Interface;
using MVP.Presenters.Strategies.Matches;

namespace Installers.LevelScene
{
    public class StrategyInstaller : Installer
    {
        protected override void InstallBindings()
        {
            // Bind each MatchStrategy implementation
            Container.BindAsTransient<IMatchStrategy>(()=> new ItemMatchStrategy());
            Container.BindAsTransient<IMatchStrategy>(()=> new BoosterMatchStrategy());
            Container.BindAsTransient<IMatchStrategy>(()=> new BoxMatchStrategy());
            Container.BindAsTransient<IMatchStrategy>(()=> new VaseMatchStrategy());
            
            // Bind each BoosterStrategy implementation
            Container.BindAsTransient<IBoosterComboStrategy>(() => Container.Construct<BombStrategy>());
            Container.BindAsTransient<IBoosterComboStrategy>(() => Container.Construct<BombBombStrategy>());
        }
    }
}
