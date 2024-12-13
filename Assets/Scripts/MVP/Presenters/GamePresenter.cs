using Cysharp.Threading.Tasks;
using MVP.Presenters.Handlers;
using UnityEngine;


namespace MVP.Presenters
{
    public class GamePresenter
    {
        private readonly ScenePresenter _scenePresenter;
        private readonly LevelTransitionHandler _levelTransitionHandler;
        public GamePresenter(ScenePresenter scenePresenter, LevelTransitionHandler levelTransitionHandler)
        {
            _scenePresenter = scenePresenter;
            _levelTransitionHandler = levelTransitionHandler;
            InitializeGame().Forget();
        }
        private async UniTask InitializeGame()
        {
            await _scenePresenter.TransitionToNextScene("MainScene", _levelTransitionHandler.SetupMainSceneRequirements);
        }

    }
}
