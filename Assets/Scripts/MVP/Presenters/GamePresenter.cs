using Cysharp.Threading.Tasks;


namespace MVP.Presenters
{
    public class GamePresenter
    {
        private readonly ScenePresenter _scenePresenter;
        public GamePresenter(ScenePresenter scenePresenter)
        {
            _scenePresenter = scenePresenter;
            InitializeGame().Forget();
        }
        private async UniTask InitializeGame()
        {
            // Load the main scene or perform other initialization tasks
            await _scenePresenter.TransitionToNextScene("LoadScene", "MainScene");
        }

    }
}
