using System;
using Cysharp.Threading.Tasks;
using DI;
using MVP.Models.Interface;
using MVP.Views.Interface;

namespace MVP.Presenters.Handlers
{
    public class LevelTransitionHandler
    {
        private readonly ILevelModel _levelModel;

        public LevelTransitionHandler(ILevelModel levelModel)
        {
            _levelModel = levelModel;
        }

        public async UniTask SetupMainSceneRequirements(Container container)
        {
            IMainUIView mainUIView = container.Resolve<IMainUIView>();

            var levelIndex = _levelModel.LevelIndex;
            var maxLevel = _levelModel.MaxLevel;
            mainUIView.SetLevelButtonText(levelIndex, maxLevel);
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
        }
        
        public async UniTask SetupLevelSceneRequirements(Container container)
        {
            LevelPresenter levelPresenter = container.Resolve<LevelPresenter>();

            await levelPresenter.LoadLevel();
        }
    }
}
