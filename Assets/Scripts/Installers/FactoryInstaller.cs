using Core.Factories;
using Core.Factories.Interface;
using DI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Installers
{
    public class FactoryInstaller : Installer
    {
        // Reference to the scene object
        [SerializeField] private ItemBlastEffectFactory _itemBlastEffectFactory; 
        [SerializeField] private ObstacleBlastEffectFactory _obstacleBlastEffectFactory; 
        [SerializeField] private BombEffectFactory _bombEffectFactory; 
        [SerializeField] private ItemFactory _itemFactory; 
        [SerializeField] private BoosterFactory _boosterFactory;
        [SerializeField] private ObstacleFactory _obstacleFactory;

        protected override void InstallBindings()
        {
            Container.BindAsSingle<IItemBlastEffectFactory>(() => _itemBlastEffectFactory);
            Container.BindAsSingle<IObstacleBlastEffectFactory>(() => _obstacleBlastEffectFactory);
            Container.BindAsSingle<IBombEffectFactory>(() => _bombEffectFactory);
            Container.BindAsSingle<IItemFactory>(() => _itemFactory);
            Container.BindAsSingle<IBoosterFactory>(() => _boosterFactory);
            Container.BindAsSingle<IObstacleFactory>(() => _obstacleFactory);
        }
    }
}
