using System;
using Core.Enum;
using Core.LevelSerialization;
using Events;
using Events.Game;
using Events.Level;
using MVP.Models.Interface;
using UnityEngine;

namespace MVP.Models
{
    public class LevelModel: ILevelModel
    {
        public LevelInfo CurrentLevelInfo { get; private set; }
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
        
        private void OnEnable()
        {
            GameEventSystem.AddListener<OnGameStartedEvent>(LoadLevel);
        }

        private void OnDisable()
        {
            GameEventSystem.RemoveListener<OnGameStartedEvent>(LoadLevel);
        }
        
        public void LoadLevel(object args)
        {
            CurrentLevelInfo = LevelSerializer.SerializeToLevelInfo(LevelIndex);
            if (CurrentLevelInfo is null)
            {
                ResetLevelIndex();
                CurrentLevelInfo = LevelSerializer.SerializeToLevelInfo(LevelIndex);
            }
            GameEventSystem.Invoke<OnLevelLoadedEvent>(new OnLevelLoadedEvent() { LevelInfo = CurrentLevelInfo });
        }
        
        private void ResetLevelIndex()
        {
            LevelIndex = 1;
        }
    }
}
