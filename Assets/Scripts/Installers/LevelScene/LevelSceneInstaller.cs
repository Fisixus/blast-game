using DI;
using UnityEngine;

namespace Installers.LevelScene
{
    public class LevelSceneInstaller : Installer
    {
        [SerializeField] private HandlerInstaller _handlerInstaller;
        [SerializeField] private StrategyInstaller _strategyInstaller;
        [SerializeField] private MVPLevelInstaller _mvpLevelInstaller;
        [SerializeField] private FactoryInstaller _factoryInstaller;
        protected override void InstallBindings()
        {
            _strategyInstaller.Install(Container);
            _factoryInstaller.Install(Container);
            _handlerInstaller.Install(Container);
            _mvpLevelInstaller.Install(Container);
        }
    }
}
