using DI;
using MVP.Views;
using MVP.Views.Interface;
using UnityEngine;

namespace Installers
{
    public class ViewInstaller : Installer
    {
        // Reference to the scene object
        [SerializeField] private GridView _gridView; 

        protected override void InstallBindings()
        {
            Container.BindAsSingle<IGridView>(() => _gridView);
        }
    }
}
