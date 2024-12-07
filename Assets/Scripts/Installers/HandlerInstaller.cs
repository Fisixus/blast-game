using DI;
using MVP.Presenters.Handlers;

namespace Installers
{
    public class HandlerInstaller : Installer
    {
        protected override void InstallBindings()
        {
            Container.BindAsSingle(() => new LevelStateHandler());
        }
    }
}
