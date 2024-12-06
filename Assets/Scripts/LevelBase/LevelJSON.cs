namespace LevelBase
{
    /// <summary>
    /// 
    /// LevelInfo is a class that is used to store the information of a level.
    /// It is serializable and compatible with JSON format to be used in the game level system.
    /// 
    /// </summary>
    [System.Serializable]
    public class LevelJSON
    {
        public int LevelNumber { get; set; }
        public int GridWidth { get; set; }
        public int GridHeight { get; set; }
        public int MoveCount { get; set; }
        public string[] Grid { get; set; }
    }
}