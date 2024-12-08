using Core.Factories.Interface;
using DI;
using MVP.Models.Interface;
using MVP.Presenters.Handlers;
using MVP.Presenters.Handlers.Strategies.Interface;
using MVP.Presenters.Handlers.Strategies.Match;

namespace Installers
{
    public class HandlerInstaller : Installer
    {
        protected override void InstallBindings()
        {
            Container.BindAsTransient<IMatchStrategy>(()=> new ItemMatchStrategy());

            Container.BindAsSingle(() => Container.Construct<LevelStateHandler>());
            Container.BindAsSingle(() => Container.Construct<MatchHandler>());
        }
    }
}
