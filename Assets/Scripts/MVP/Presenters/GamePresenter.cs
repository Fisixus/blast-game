using UnityEngine;

namespace MVP.Presenters
{
    public class GamePresenter : MonoBehaviour
    {
        public void Awake()
        {
            StartLevel();
        }
        
        //TODO:Scene Transitions

        private void StartLevel()
        {
            //TODO:m_SignalBus.Fire(new GameStartSignal() { });
        }
    }
}
