using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MVP.Views.Interface
{
    public interface IMainUIView
    {
        public Button NewLevelButton { get;  }
        public TextMeshProUGUI LevelButtonText { get;  }

        public void SetLevelButtonText(int level, int maxLevel);
    }
}
