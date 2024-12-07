using Core.LevelSerialization;

namespace Events.Level
{
    public class OnLevelLoadedEvent : Event
    {
        public LevelInfo LevelInfo;
    }
}
