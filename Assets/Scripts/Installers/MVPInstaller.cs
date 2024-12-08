using DI;
using MVP.Models;
using MVP.Models.Interface;
using MVP.Presenters;
using MVP.Views;
using MVP.Views.Interface;
using UnityEngine;

namespace Installers
{
    [DefaultExecutionOrder(-4)]
    public class MVPInstaller : Installer
    {
        [SerializeField] private GridView _gridView;
        protected override void InstallBindings()
        {
        
            Container.BindAsSingle<IGridModel>(() => Container.Construct<GridModel>());
            Container.BindAsSingle<IGridView>(() => _gridView);

            Container.BindAsSingleNonLazy(() => Container.Construct<LevelPresenter>());
            Container.BindAsSingleNonLazy(() => Container.Construct<MatchPresenter>());
            Container.BindAsSingleNonLazy<ILevelModel>(() => Container.Construct<LevelModel>());
            Container.BindAsSingleNonLazy(() => Container.Construct<GamePresenter>());

        }
    }
}
