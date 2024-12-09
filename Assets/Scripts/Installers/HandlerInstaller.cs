using DI;
using MVP.Presenters.Handlers;

namespace Installers
{
    public class HandlerInstaller : Installer
    {
        protected override void InstallBindings()
        {
            Container.BindAsSingle(() => Container.Construct<LevelStateHandler>());
            Container.BindAsSingle(() => Container.Construct<MatchHandler>());
            Container.BindAsSingle(() => Container.Construct<GridShiftHandler>());
        }
    }
}
