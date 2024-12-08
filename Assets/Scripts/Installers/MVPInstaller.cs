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
            // Instead of manually creating and resolving dependencies,
            // use Construct<T> to automatically resolve constructor parameters.
        
            // If GamePresenter and LevelPresenter implement IPreInitializable,
            // PreInitialize will be called right after construction.
        
            // Use NonLazy for LevelModel so that it's created immediately after installation
            Container.BindAsSingle<IGridModel>(() => Container.Construct<GridModel>());
            Container.BindAsSingle<IGridView>(() => _gridView);

// Now, after IGridModel is bound, we can safely create non-lazy objects that depend on it
            Container.BindAsSingleNonLazy(() => Container.Construct<LevelPresenter>());
            Container.BindAsSingleNonLazy<ILevelModel>(() => Container.Construct<LevelModel>());
            Container.BindAsSingleNonLazy(() => Container.Construct<GamePresenter>());

            // No need to call Resolve manually!
            // Non-lazy binding ensures LevelModel is created now, and GamePresenter/LevelPresenter 
            // will be created when first needed or can also be made non-lazy if desired.
        }
    }
}
