using Core.GridElements.GridPawns;

namespace MVP.Presenters.Handlers
{
    public class HintHandler
    {
        private BaseGridObject[,] _grid;
        
        public void Initialize(BaseGridObject[,] grid)
        {
            _grid = grid;
        }
    }
}
