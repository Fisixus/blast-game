using Core.Factories;
using Core.Factories.Interface;
using DI;
using UnityEngine;

namespace Installers
{
    public class FactoryInstaller : Installer
    {
        // Reference to the scene object
        [SerializeField] private ItemFactory _itemFactory; 
        [SerializeField] private BoosterFactory _boosterFactory;

        protected override void InstallBindings()
        {
            Container.BindAsSingle<IItemFactory>(() => _itemFactory);
            Container.BindAsSingle<IBoosterFactory>(() => _boosterFactory);
        }
    }
}
