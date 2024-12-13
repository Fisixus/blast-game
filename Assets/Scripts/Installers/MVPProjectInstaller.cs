using DI;
using MVP.Models;
using MVP.Models.Interface;
using MVP.Presenters;

namespace Installers
{
    public class MVPProjectInstaller : Installer
    {
        protected override void InstallBindings()
        {
            Container.BindAsSingleNonLazy(() => Container.Construct<ScenePresenter>());
            Container.BindAsSingleNonLazy(() => Container.Construct<GamePresenter>());
            Container.BindAsSingleNonLazy(() => Container.Construct<LevelPresenter>());
            Container.BindAsSingleNonLazy<ILevelModel>(() => Container.Construct<LevelModel>());
        }
    }
}
