using DI;
using MVP.Models;

namespace Installers
{
    public class ModelInstaller : Installer
    {
        protected override void InstallBindings()
        {
            Container.BindAsSingle(() => new LevelModel());
            Container.BindAsSingle(() => new GridModel());
        }
    }
}
