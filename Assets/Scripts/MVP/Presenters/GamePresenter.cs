using Events;
using Events.Game;
using UnityEngine;

namespace MVP.Presenters
{
    public class GamePresenter
    {
        public GamePresenter()
        {
            StartGame();
        }
        
        private void StartGame() => GameEventSystem.Invoke<OnGameStartedEvent>();
    }
}
