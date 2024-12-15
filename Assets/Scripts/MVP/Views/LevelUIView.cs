using System;
using Core.GridElements.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DI.Contexts;
using MVP.Helpers;
using MVP.Presenters.Handlers;
using MVP.Views.Interface;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MVP.Views
{
    public class LevelUIView : MonoBehaviour, ILevelUIView
    {
        [field: SerializeField] public Button EscapeButton { get; private set; }
        [field: SerializeField] public Button RetryLevelButton { get; private set; }
        [field: SerializeField] public TextMeshProUGUI MoveCounterText { get; private set; }
        [field: SerializeField] public Transform GoalParentTr { get; private set; }
        [field: SerializeField] public Transform SuccessPanelTr { get; private set; }
        [field: SerializeField] public ParticleSystem StarPS { get; private set; }
        [field: SerializeField] public Transform FailPanelTr { get; private set; }
        
        private void Awake()
        {
            RetryLevelButton.onClick.AddListener(() =>
            {
                RetryLevel().Forget();
            });
            EscapeButton.onClick.AddListener(() =>
            {
                EscapeLevel().Forget();
            });
        }

        private void OnDisable()
        {
            RetryLevelButton.onClick.RemoveAllListeners();
            EscapeButton.onClick.RemoveAllListeners();
        }

        private async UniTask EscapeLevel()
        {
            try
            {
                // Disable the button to prevent multiple clicks
                EscapeButton.interactable = false;
                await SceneTransitionHelper.PerformTransition(
                    "MainScene",
                    async (container) =>
                    {
                        // Specific setup logic for this scene
                        var levelTransitionHandler = ProjectContext.Container.Resolve<SceneTransitionHandler>();
                        await levelTransitionHandler.SetupMainSceneRequirements(container);
                    });
            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }

        private async UniTask RetryLevel()
        {
            try
            {
                // Disable the button to prevent multiple clicks
                RetryLevelButton.interactable = false;
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


        public void InitializeGoal(Texture newTexture, Vector2 newSize, GoalUI goalUI, LevelGoal goal)
        {
            var ps = goalUI.GetComponentInChildren<ParticleSystem>();
            var mainImg = goalUI.GetComponent<RawImage>();
            var checkedImg = goalUI.GetComponentInChildren<Image>();
            var countText = goalUI.GetComponentInChildren<TextMeshProUGUI>();

            // Initialize UI elements
            ResetGoalUI(mainImg, checkedImg, countText, newTexture, newSize, goal.Count);

            // Subscribe to goal updates
            goal.OnUIGoalUpdated += (sender, updatedCount) =>
                UpdateGoalCount(countText, ps, checkedImg, mainImg, updatedCount);
        }

        public async UniTask OpenSuccessPanel(float duration)
        {
            await TogglePanel(SuccessPanelTr, true, duration);
            StarPS.Play();
            await UniTask.Delay(TimeSpan.FromSeconds(4)); 
            StarPS.Stop();
            await SceneTransitionHelper.PerformTransition(
                "MainScene",
                async (container) =>
                {
                    // Specific setup logic for this scene
                    var levelTransitionHandler = ProjectContext.Container.Resolve<SceneTransitionHandler>();
                    await levelTransitionHandler.SetupMainSceneRequirements(container);
                });
        }

        public void CloseSuccessPanel(float duration)
        {
            TogglePanel(SuccessPanelTr, false, duration).Forget();
        }

        public void OpenFailPanel(float duration)
        {
            TogglePanel(FailPanelTr, true, duration).Forget();
            
        }

        public void CloseFailPanel(float duration)
        {
            TogglePanel(FailPanelTr, false, duration).Forget();
        }

        public void SetMoveCounter(int numberOfMoves)
        {
            MoveCounterText.text = numberOfMoves.ToString();
        }

        private void ResetGoalUI(RawImage mainImg, Image checkedImg, TextMeshProUGUI countText, Texture texture,
            Vector2 size, int count)
        {
            // Set initial properties for UI elements
            checkedImg.transform.localScale = Vector3.zero;
            countText.transform.localScale = Vector3.one;
            mainImg.rectTransform.sizeDelta = size;
            mainImg.texture = texture;
            countText.text = count.ToString();
        }

        private void UpdateGoalCount(TextMeshProUGUI countText, ParticleSystem ps, Image checkedImg, RawImage mainImg,
            int updatedCount)
        {
            if (!int.TryParse(countText.text, out var currentCount) || currentCount == updatedCount)
                return; // Exit if count is unchanged

            // Update UI when goal is complete
            if (updatedCount == 0) AnimateGoalCompletion(countText, checkedImg);

            countText.text = updatedCount.ToString();

            // Play particle effect and animate the main image if not already playing
            if (!ps.isPlaying)
            {
                mainImg.transform.DOScale(Vector3.one * 1.25f, 0.5f).SetLoops(2, LoopType.Yoyo);
                ps.Play();
            }
        }

        private void AnimateGoalCompletion(TextMeshProUGUI countText, Image checkedImg)
        {
            checkedImg.transform.DOScale(Vector3.one, 0.25f);
            countText.transform.DOScale(Vector3.zero, 0.15f);
        }

        private async UniTask TogglePanel(Transform panelTransform, bool isOpen, float duration)
        {
            var cg = panelTransform.GetComponent<CanvasGroup>();
            if (isOpen)
            {
                cg.interactable = true;
                cg.blocksRaycasts = true;
                DOTween.To(() => cg.alpha, x => cg.alpha = x, 1, duration);
            }
            else
            {
                cg.alpha = 0;
                cg.interactable = false;
                cg.blocksRaycasts = false;
            }
            await UniTask.Delay(TimeSpan.FromSeconds(duration), DelayType.DeltaTime); 
        }


    }
}
