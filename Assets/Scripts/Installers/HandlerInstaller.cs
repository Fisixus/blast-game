using Core.Factories.Interface;
using DI;
using MVP.Models.Interface;
using MVP.Presenters.Handlers;

namespace Installers
{
    public class HandlerInstaller : Installer
    {
        protected override void InstallBindings()
        {
            Container.BindAsSingle(() => new LevelStateHandler
            (Container.Resolve<IItemFactory>(), 
            Container.Resolve<IBoosterFactory>(), 
                Container.Resolve<IGridModel>())
            );
        }
    }
}
