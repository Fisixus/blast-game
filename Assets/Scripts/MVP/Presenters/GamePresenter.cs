using Cysharp.Threading.Tasks;
using DI.Contexts;
using Events;
using Events.Game;
using UnityEngine;

namespace MVP.Presenters
{
    public class GamePresenter
    {
        private readonly ScenePresenter _scenePresenter;
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            var prefab = Resources.Load<GameObject>("Contexts/ProjectContext");
            if (prefab == null)
            {
                Debug.LogError("Failed to load ProjectContext prefab from Resources/Contexts!");
                return;
            }
            Object.Instantiate(prefab);
        }

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
