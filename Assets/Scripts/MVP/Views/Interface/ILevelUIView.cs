using Core.GridElements.UI;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MVP.Views.Interface
{
    public interface ILevelUIView 
    {
        public Button EscapeButton { get;  }
        public Button RetryLevelButton { get;  }
        public TextMeshProUGUI MoveCounterText { get; }
        public Transform GoalParentTr { get; }
        public Transform SuccessPanelTr { get; }
        public ParticleSystem StarPS { get;}
        public Transform FailPanelTr { get; }
        public void SetMoveCounter(int numberOfMoves);
        public void InitializeGoal(Texture newTexture, Vector2 newSize, GoalUI goalUI, LevelGoal goal);

        public UniTask OpenSuccessPanel(float duration = 1f);
        public void CloseSuccessPanel(float duration = 0f);
        public void OpenFailPanel(float duration = 1f);
        public void CloseFailPanel(float duration = 0f);
    }
}
