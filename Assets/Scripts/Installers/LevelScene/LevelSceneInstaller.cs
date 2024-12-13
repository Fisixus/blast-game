using DI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Installers.LevelScene
{
    public class LevelSceneInstaller : Installer
    {
        [SerializeField] private HandlerInstaller _handlerInstaller;
        [SerializeField] private StrategyInstaller _strategyInstaller;
        [FormerlySerializedAs("_mvpInstaller")] [SerializeField] private MVPLevelInstaller mvpLevelInstaller;
        [SerializeField] private FactoryInstaller _factoryInstaller;
        protected override void InstallBindings()
        {
            _strategyInstaller.Install(Container);
            _factoryInstaller.Install(Container);
            _handlerInstaller.Install(Container);
            mvpLevelInstaller.Install(Container);
        }
    }
}
