using DI;
using MVP.Presenters.Handlers.Strategies.Interface;
using MVP.Presenters.Handlers.Strategies.Match;

namespace Installers
{
    public class StrategyInstaller : Installer
    {
        protected override void InstallBindings()
        {
            // Bind each MatchStrategy implementation
        }
    }
}
