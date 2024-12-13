using System.Text;
using DI;
using Events;
using Events.Level;
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
            NewLevelButton.onClick.AddListener(RequestLevel);
        }
        
        private void RequestLevel()
        {
            NewLevelButton.interactable = false;
            GameEventSystem.Invoke<OnLevelRequestedEvent>();
            //RetryLevelButton.interactable = false;
            //m_SignalBus.Fire(new OnLevelRequestedSignal() { });
        }

        public void SetLevelButtonText(int level)
        {
            StringBuilder stringBuilder = new StringBuilder("Level");
            stringBuilder.Append(level);
            LevelButtonText.text = stringBuilder.ToString();
        }
        
    }
}
