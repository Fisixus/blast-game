using Core.LevelSerialization;

namespace MVP.Models.Interface
{
    public interface ILevelModel
    {
        public int LevelIndex { get; set; }
        public int MaxLevel { get; }

        public LevelInfo LoadLevel(); 
    }
}
