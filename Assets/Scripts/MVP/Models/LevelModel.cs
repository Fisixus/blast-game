using Core.LevelSerialization;
using MVP.Models.Interface;
using UnityEngine;

namespace MVP.Models
{
    public class LevelModel: ILevelModel
    {
        public int MaxLevel
        {
            get
            {
                var jsonFiles = Resources.LoadAll<TextAsset>("Levels");
                return jsonFiles?.Length ?? 0; // Return 1 if jsonFiles is null
            }
        }

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
