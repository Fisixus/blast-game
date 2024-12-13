using DI;
using MVP.Models;
using MVP.Models.Interface;
using MVP.Presenters;
using MVP.Presenters.Handlers;

namespace Installers
{
    public class MVPProjectInstaller : Installer
    {
        protected override void InstallBindings()
        {
            Container.BindAsSingle(() => Container.Construct<ScenePresenter>());
            Container.BindAsSingle(() => Container.Construct<LevelPresenter>());
            Container.BindAsSingle(() => Container.Construct<LevelTransitionHandler>());
            Container.BindAsSingle<ILevelModel>(() => Container.Construct<LevelModel>());
            Container.BindAsSingleNonLazy(() => Container.Construct<GamePresenter>());
        }
    }
}
