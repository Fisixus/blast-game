using LevelBase;

namespace MVP.Models.Interface
{
    public interface ILevelModel
    {
        public LevelInfo LevelInfo { get; }
        public int LevelIndex { get; set; }
        public void LoadLevel(); 
    }
}
