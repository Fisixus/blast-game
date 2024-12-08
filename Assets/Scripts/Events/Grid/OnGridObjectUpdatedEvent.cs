using Core.GridElements.GridPawns;

namespace Events.Grid
{
    public class OnGridObjectUpdatedEvent : Event
    {
        public BaseGridObject GridObject;
        public bool IsAnimationOn = true;
    }
}
