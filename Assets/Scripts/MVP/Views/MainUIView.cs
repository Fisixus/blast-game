using System;
using System.Text;
using Cysharp.Threading.Tasks;
using DI;
using DI.Contexts;
using Events;
using Events.Level;
using MVP.Presenters;
using MVP.Presenters.Handlers;
using MVP.Views.Interface;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MVP.Views
{
    public class MainUIView : MonoBehaviour, IMainUIView, IPreInitializable
    {
        [field:SerializeField]public Button NewLevelButton { get; private set; }
        [field:SerializeField]public TextMeshProUGUI LevelButtonText { get; private set; }
        
        public void PreInitialize()
        {
            NewLevelButton.onClick.AddListener(() =>
            {
                RequestLevel().Forget();
            });
        }

        private void OnDisable()
        {
            NewLevelButton.onClick.RemoveAllListeners();
        }

        private async UniTask RequestLevel()
        {
            try
            {
                // Disable the button to prevent multiple clicks
                NewLevelButton.interactable = false;

                // Resolve dependencies
                var levelTransitionHandler = ProjectContext.Container.Resolve<LevelTransitionHandler>();
                var scenePresenter = ProjectContext.Container.Resolve<ScenePresenter>();

                // Perform scene transition
                Debug.Log("Starting Level Transition...");
                await scenePresenter.TransitionToNextScene("LevelScene", async (container) =>
                {
                    await levelTransitionHandler.SetupLevelSceneRequirements(container);
                });

                Debug.Log("Level Transition Complete!");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error during level transition: {ex.Message}");
            }
        }

        public void SetLevelButtonText(int level, int maxLevel)
        {
            StringBuilder stringBuilder;
            if (level > maxLevel)
            {
                NewLevelButton.interactable = false;
                stringBuilder = new StringBuilder("Finished");
                NewLevelButton.onClick.RemoveAllListeners();
            }
            else
            {
                NewLevelButton.interactable = true;
                stringBuilder = new StringBuilder("Level");
                stringBuilder.Append(level);
            }
            LevelButtonText.text = stringBuilder.ToString();
        }
        
    }
}
