using Cysharp.Threading.Tasks;
using DI;
using DI.Contexts;
using MVP.Models.Interface;
using MVP.Views.Interface;

namespace MVP.Presenters.Handlers
{
    public class SceneTransitionHandler
    {
        private readonly ILevelModel _levelModel;

        public SceneTransitionHandler()
        {
            _levelModel = ProjectContext.Container.Resolve<ILevelModel>();
        }

        public async UniTask SetupMainSceneRequirements(Container container)
        {
            IMainUIView mainUIView = container.Resolve<IMainUIView>();

            var levelIndex = _levelModel.LevelIndex;
            var maxLevel = _levelModel.MaxLevel;
            mainUIView.SetLevelButtonText(levelIndex, maxLevel);
            // Future async work can go here
            await UniTask.CompletedTask;
        }

        public async UniTask SetupLevelSceneRequirements(Container container)
        {
            LevelPresenter levelPresenter = container.Resolve<LevelPresenter>();
            await levelPresenter.LoadLevel();
        }
    }
}