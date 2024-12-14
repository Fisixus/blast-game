using Cysharp.Threading.Tasks;
using MVP.Presenters.Handlers;

namespace MVP.Presenters
{
    public class GamePresenter
    {
        private readonly ScenePresenter _scenePresenter;
        private readonly SceneTransitionHandler _sceneTransitionHandler;
        public GamePresenter(ScenePresenter scenePresenter, SceneTransitionHandler sceneTransitionHandler)
        {
            _scenePresenter = scenePresenter;
            _sceneTransitionHandler = sceneTransitionHandler;
            InitializeGame().Forget();
        }
        private async UniTask InitializeGame()
        {
            await _scenePresenter.TransitionToNextScene("MainScene", async (container) =>
            {
                await _sceneTransitionHandler.SetupMainSceneRequirements(container);
            });
        }

    }
}
