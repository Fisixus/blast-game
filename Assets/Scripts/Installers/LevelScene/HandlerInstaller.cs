using DI;
using MVP.Presenters.Handlers;
using MVP.Presenters.Handlers.Effects;

namespace Installers.LevelScene
{
    public class HandlerInstaller : Installer
    {
        protected override void InstallBindings()
        {
            Container.BindAsSingle(() => Container.Construct<GridObjectFactoryHandler>());
            Container.BindAsSingle(() => Container.Construct<GoalUIHandler>());
            Container.BindAsSingle(() => Container.Construct<GoalHandler>());
            Container.BindAsSingle(() => Container.Construct<BombEffectHandler>());
            Container.BindAsSingle(() => Container.Construct<BlastEffectHandler>());
            Container.BindAsSingle(() => Container.Construct<LevelSetupHandler>());
            Container.BindAsSingle(() => Container.Construct<MatchHandler>());
            Container.BindAsSingle(() => Container.Construct<BoosterHandler>());
            Container.BindAsSingle(() => Container.Construct<ComboHandler>());
            Container.BindAsSingle(() => Container.Construct<HintHandler>());
            Container.BindAsSingle(() => Container.Construct<GridShiftHandler>());
        }
    }
}