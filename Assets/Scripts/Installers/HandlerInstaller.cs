using DI;
using MVP.Presenters.Handlers;

namespace Installers
{
    public class HandlerInstaller : Installer
    {
        protected override void InstallBindings()
        {
            Container.BindAsSingle(() => Container.Construct<GridObjectFactoryHandler>());
            Container.BindAsSingle(() => Container.Construct<LevelStateHandler>());
            Container.BindAsSingle(() => Container.Construct<MatchHandler>());
            Container.BindAsSingle(() => Container.Construct<BoosterHandler>());
            Container.BindAsSingle(() => Container.Construct<HintHandler>());
            Container.BindAsSingle(() => Container.Construct<GridShiftHandler>());
        }
    }
}
