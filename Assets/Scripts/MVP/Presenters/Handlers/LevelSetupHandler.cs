using System.Collections.Generic;
using Core.GridElements.GridPawns;
using Core.LevelSerialization;
using MVP.Models.Interface;

namespace MVP.Presenters.Handlers
{
    public class LevelSetupHandler
    {
        private readonly IGridModel _gridModel;
        private readonly GridObjectFactoryHandler _gridObjectFactoryHandler;
        private readonly HintHandler _hintHandler;
        private readonly BoosterHandler _boosterHandler;
        private readonly MatchHandler _matchHandler;
        
        public LevelSetupHandler(GridObjectFactoryHandler gridObjectFactoryHandler, IGridModel gridModel,
            HintHandler hintHandler, BoosterHandler boosterHandler, MatchHandler matchHandler)
        {
            _gridObjectFactoryHandler = gridObjectFactoryHandler;
            _gridModel = gridModel;
            _hintHandler = hintHandler;
            _boosterHandler = boosterHandler;
            _matchHandler = matchHandler;
        }

        public void Initialize(LevelInfo levelInfo)
        {
            // Process the grid in a single loop
            var gridObjectTypes = levelInfo.GridObjectTypes;
            var rows = gridObjectTypes.GetLength(0);
            var cols = gridObjectTypes.GetLength(1);

            List<BaseGridObject> gridObjects = new List<BaseGridObject>(100);
            _gridObjectFactoryHandler.DestroyAllGridObjects();
            _gridObjectFactoryHandler.PopulateGridWithObjects(gridObjectTypes, gridObjects);
            _gridModel.Initialize(gridObjects, cols, rows);
            // Set up match and effect handlers
            _matchHandler.Initialize(_gridModel.Grid);
            _boosterHandler.Initialize(_gridModel.Grid);
            _hintHandler.Initialize(_gridModel.Grid);
        }
        
    }
}