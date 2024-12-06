using System;
using Core.Enum;
using Events;
using Events.Game;
using LevelBase;
using MVP.Models.Interface;
using UnityEngine;

namespace MVP.Models
{
    public class LevelModel: MonoBehaviour, ILevelModel
    {
        public LevelInfo LevelInfo { get; private set; }
        public int LevelIndex
        {
            get
            {
                var levelIndex = Mathf.Max(1, PlayerPrefs.GetInt(LevelIndexStr));
                return levelIndex;
            }
            set
            {
                PlayerPrefs.SetInt(LevelIndexStr, value);
                PlayerPrefs.Save();
            }
        }

        private ItemType[,] _gridData;
        private const string LevelIndexStr = "LevelIndex";

        // [Inject]
        // private void Construct(SignalBus signalBus)
        // {
        //     m_SignalBus = signalBus;
        //
        //     m_SignalBus.Subscribe<GameStartSignal>(OnLevelStarted);
        // }

        private void OnEnable()
        {
            GameEventSystem.AddListener<OnGameStartedEvent>(LoadLevel);
        }

        private void OnDisable()
        {
            GameEventSystem.RemoveListener<OnGameStartedEvent>(LoadLevel);
        }

        // private void OnLevelStarted(GameStartSignal args)
        // {
        //     LoadLevel();
        // }

        public void LoadLevel(object args)
        {
            LevelInfo = LevelSerializer.SerializeToLevelInfo(LevelIndex);
            if (LevelInfo is null)
            {
                ResetLevelIndex();
                LevelInfo = LevelSerializer.SerializeToLevelInfo(LevelIndex);
            }
            //TODO:m_SignalBus.Fire(new OnLevelLoadSignal() { Level = CurrentLevel });
        }
        
        private void ResetLevelIndex()
        {
            LevelIndex = 1;
        }
    }
}
