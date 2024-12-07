using DI;
using MVP.Models;
using MVP.Views;
using MVP.Views.Interface;
using UnityEngine;

namespace Installers
{
    public class GridInstaller : Installer
    {
        // Reference to the scene object
        [SerializeField] private GridView _gridView; 

        protected override void InstallBindings()
        {
            Container.BindAsSingle<IGridView>(() => _gridView);
            Container.BindAsSingle(() => new GridModel());
        }
    }
}
