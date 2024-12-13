using System;
using Cysharp.Threading.Tasks;
using DI;
using DI.Contexts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MVP.Presenters
{
    public class ScenePresenter
    {
        private const string LoadingSceneName = "LoadScene";

        public async UniTask TransitionToNextScene(string nextScene, Func<Container, UniTask> setupTask)
        {
            await LoadNextSceneAsync(nextScene, setupTask);
        }
    
        private async UniTask LoadNextSceneAsync(string nextSceneName, Func<Container, UniTask> setupTask)
        {
            var currentSceneName = SceneManager.GetActiveScene().name;
            
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
                //Debug.Log($"Loading {nextSceneName} progress: {loadOp.progress * 100}%");
                await UniTask.Yield();
            }

            // Set the new scene as active
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(nextSceneName));
            var sceneContext = FindSceneContextInActiveScene();//TODO:
            if (sceneContext == null)
            {
                Debug.LogError("SceneContext not found in the loaded scene.");
                return;
            }
            // Pass the container to the setup task
            var sceneContainer = sceneContext.SceneContainer;
            await setupTask(sceneContainer);
            
            // Unload the current scene
            await SceneManager.UnloadSceneAsync(currentSceneName);

            // Deactivate the loading screen
            SetLoadingSceneActive(false);
        }
        
        private SceneContext FindSceneContextInActiveScene()
        {
            var activeScene = SceneManager.GetActiveScene();
            foreach (var rootGameObject in activeScene.GetRootGameObjects())
            {
                var context = rootGameObject.GetComponent<SceneContext>();
                if (context != null)
                {
                    return context;
                }
            }
            return null;
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