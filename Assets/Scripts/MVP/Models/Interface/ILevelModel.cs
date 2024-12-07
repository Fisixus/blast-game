using Core.LevelSerialization;

namespace MVP.Models.Interface
{
    public interface ILevelModel
    {
        public LevelInfo CurrentLevelInfo { get; }
        public int LevelIndex { get; set; }
        public void LoadLevel(object args); 
    }
}
