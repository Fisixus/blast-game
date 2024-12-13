using System;
using Cysharp.Threading.Tasks;
using DI;
using DI.Contexts;
using MVP.Presenters;
using MVP.Presenters.Handlers;
using UnityEngine;

namespace MVP.Helpers
{
    public static class SceneTransitionHelper
    {
        public static async UniTask PerformTransition(
            string sceneName,
            Func<Container, UniTask> setupAction)
        {
            try
            {
                // Resolve dependencies
                var scenePresenter = ProjectContext.Container.Resolve<ScenePresenter>();

                // Perform scene transition
                Debug.Log($"Starting transition to {sceneName}...");
                await scenePresenter.TransitionToNextScene(sceneName, async (container) =>
                {
                    await setupAction(container);
                });
                Debug.Log($"Transition to {sceneName} complete!");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error during scene transition: {ex.Message}");
            }
        }
    }
}