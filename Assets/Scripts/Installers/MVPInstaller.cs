using DI;
using MVP.Models;
using MVP.Models.Interface;
using MVP.Presenters;
using MVP.Views;
using MVP.Views.Interface;
using UnityEngine;

namespace Installers
{
    public class MVPInstaller : Installer
    {
        [SerializeField] private GridView _gridView;
        [SerializeField] private LevelUIView _levelUIView;
        protected override void InstallBindings()
        {
        
            Container.BindAsSingle<IGridModel>(() => Container.Construct<GridModel>());
            Container.BindAsSingle<IGridView>(() => _gridView);
            Container.BindAsSingle<ILevelUIView>(() => _levelUIView);

            Container.BindAsSingleNonLazy(() => Container.Construct<LevelPresenter>());
            Container.BindAsSingleNonLazy(() => Container.Construct<GridPresenter>());
            Container.BindAsSingleNonLazy<ILevelModel>(() => Container.Construct<LevelModel>());
            Container.BindAsSingleNonLazy(() => Container.Construct<GamePresenter>());

        }
    }
}
