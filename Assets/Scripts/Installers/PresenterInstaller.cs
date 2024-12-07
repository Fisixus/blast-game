using DI;
using MVP.Presenters;
using UnityEngine;

namespace Installers
{
    public class PresenterInstaller : Installer
    {
        protected override void InstallBindings()
        {
            Container.BindAsSingle(() => new GamePresenter());
            Container.BindAsSingle(() => new LevelPresenter());
        }
    }
}
