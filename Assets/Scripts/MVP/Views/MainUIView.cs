using System;
using System.Text;
using Cysharp.Threading.Tasks;
using DI;
using DI.Contexts;
using MVP.Helpers;
using MVP.Presenters;
using MVP.Presenters.Handlers;
using MVP.Views.Interface;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MVP.Views
{
    public class MainUIView : MonoBehaviour, IMainUIView
    {
        [field:SerializeField]public Button NewLevelButton { get; private set; }
        [field:SerializeField]public TextMeshProUGUI LevelButtonText { get; private set; }

        private void Awake()
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
                // Use the reusable scene transition logic
                await SceneTransitionHelper.PerformTransition(
                    "LevelScene",
                    async (container) =>
                    {
                        // Specific setup logic for this scene
                        var levelTransitionHandler = ProjectContext.Container.Resolve<SceneTransitionHandler>();
                        await levelTransitionHandler.SetupLevelSceneRequirements(container);
                    });
            }
            catch (Exception e)
            {
                throw new Exception();
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
