using Events;
using Events.Game;
using UnityEngine;

namespace MVP.Presenters
{
    public class GamePresenter
    {
        public GamePresenter()
        {
            Debug.Log("SA");
            GameEventSystem.Invoke<OnGameStartedEvent>();
        }
    }
}
