using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MVP.Presenters
{
    public class ScenePresenter
    {
        private const string LoadingSceneName = "LoadScene";

        public async UniTask TransitionToNextScene(string currentScene, string nextScene)
        {
            await LoadNextSceneAsync(currentScene, nextScene);
        }
    
        private async UniTask LoadNextSceneAsync(string currentSceneName, string nextSceneName)
        {
            // Ensure LoadScene is loaded and activate its UI
            if (!SceneManager.GetSceneByName(LoadingSceneName).isLoaded)
            {
                await SceneManager.LoadSceneAsync(LoadingSceneName, LoadSceneMode.Additive);
            }
            SetLoadingSceneActive(true);

            // Load the next scene
            var loadOp = SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Additive);
            while (!loadOp.isDone)
            {
                Debug.Log($"Loading {nextSceneName} progress: {loadOp.progress * 100}%");
                await UniTask.Yield();
            }

            // Set the new scene as active
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(nextSceneName));

            // Unload the current scene
            await SceneManager.UnloadSceneAsync(currentSceneName);

            // Deactivate the loading screen
            SetLoadingSceneActive(false);
        }

        private void SetLoadingSceneActive(bool isActive)
        {
            var loadingScene = SceneManager.GetSceneByName(LoadingSceneName);
            if (loadingScene.isLoaded)
            {
                foreach (var rootGameObject in loadingScene.GetRootGameObjects())
                {
                    rootGameObject.SetActive(isActive);
                }
            }
        }
    }
}