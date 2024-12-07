using DI;
using MVP.Models;
using MVP.Models.Interface;
using MVP.Presenters;
using MVP.Presenters.Handlers;
using MVP.Views;
using MVP.Views.Interface;
using UnityEngine;

namespace Installers
{
    public class MVPInstaller : Installer
    {
        [SerializeField] private GridView _gridView;
        protected override void InstallBindings()
        {
            Container.BindAsSingle(() => new GamePresenter());
            Container.BindAsSingle(() => new LevelPresenter(
                Container.Resolve<LevelStateHandler>(), 
                    Container.Resolve<IGridView>()
                )
            );
            Container.BindAsSingleNonLazy<ILevelModel>(() => new LevelModel());
            Container.BindAsSingle<IGridModel>(() => new GridModel());
            Container.BindAsSingle<IGridView>(() => _gridView);


            Container.Resolve<LevelPresenter>();
            Container.Resolve<GamePresenter>();
        }
    }
}
