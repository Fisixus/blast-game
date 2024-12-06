using Events;
using Events.Game;
using UnityEngine;

namespace MVP.Presenters
{
    public class GamePresenter : MonoBehaviour
    {
        public void OnEnable()
        {
            StartLevel();
        }
        
        //TODO:Scene Transitions

        private void StartLevel()
        {
            GameEventSystem.Invoke<OnGameStartedEvent>();
        }
    }
}
