using DI;
using UnityEngine;

namespace Installers
{
    public class MainInstaller : Installer
    {
        [SerializeField] private HandlerInstaller _handlerInstaller;
        [SerializeField] private StrategyInstaller _strategyInstaller;
        [SerializeField] private MVPInstaller _mvpInstaller;
        [SerializeField] private FactoryInstaller _factoryInstaller;
        protected override void InstallBindings()
        {
            _strategyInstaller.Install(Container);
            _factoryInstaller.Install(Container);
            _handlerInstaller.Install(Container);
            _mvpInstaller.Install(Container);
        }
    }
}
