using DI;
using MVP.Models;
using MVP.Presenters.Handlers;

namespace Installers
{
    public class LevelInstaller : Installer
    {
        protected override void InstallBindings()
        {
            Container.BindAsSingle(() => new LevelModel());
        }
    }
}
