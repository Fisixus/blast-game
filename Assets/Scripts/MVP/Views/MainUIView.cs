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
    }
}
