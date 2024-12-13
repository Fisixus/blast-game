using System;
using Core.GridElements.Enums;
using Core.LevelSerialization;
using Events;
using Events.Level;
using MVP.Models.Interface;
using UnityEngine;

namespace MVP.Models
{
    public class LevelModel: ILevelModel
    {

        public int MaxLevel { get; private set; } = 10;//TODO:
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

        private const string LevelIndexStr = "LevelIndex";

        public LevelModel()
        {
        }

        ~LevelModel()
        {
        }
        
        public LevelInfo LoadLevel()
        {
            var currentLevelInfo = LevelSerializer.SerializeToLevelInfo(LevelIndex);
            if (currentLevelInfo is null)
            {
                ResetLevelIndex();
                currentLevelInfo = LevelSerializer.SerializeToLevelInfo(LevelIndex);
            }

            return currentLevelInfo;
        }
        
        private void ResetLevelIndex()
        {
            LevelIndex = 1;
        }
    }
}
