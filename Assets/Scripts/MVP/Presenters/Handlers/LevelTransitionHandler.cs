using System;
using Cysharp.Threading.Tasks;
using DI;
using DI.Contexts;
using MVP.Models.Interface;
using MVP.Views.Interface;
using UnityEngine;

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

            // Resolve MainUIView from the scene's container
            var levelIndex = _levelModel.LevelIndex;
            mainUIView.SetLevelButtonText(levelIndex);
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
        }
    }
}
