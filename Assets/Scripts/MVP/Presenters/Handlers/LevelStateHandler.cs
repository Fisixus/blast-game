using System.Collections.Generic;
using Core.GridElements.GridPawns;
using Core.LevelSerialization;
using MVP.Models.Interface;
using MVP.Views.Interface;
using UTasks;

namespace MVP.Presenters.Handlers
{
    public class LevelStateHandler
    {
        private readonly IGridModel _gridModel;
        private readonly GridObjectFactoryHandler _gridObjectFactoryHandler;
        private readonly HintHandler _hintHandler;
        private readonly BoosterHandler _boosterHandler;
        private readonly MatchHandler _matchHandler;
        private readonly ILevelUIView _levelUIView;
        
        public LevelStateHandler(GridObjectFactoryHandler gridObjectFactoryHandler, IGridModel gridModel,
            HintHandler hintHandler, BoosterHandler boosterHandler, MatchHandler matchHandler, ILevelUIView levelUIView)
        {
            _gridObjectFactoryHandler = gridObjectFactoryHandler;
            _gridModel = gridModel;
            _hintHandler = hintHandler;
            _boosterHandler = boosterHandler;
            _matchHandler = matchHandler;
            _levelUIView = levelUIView;
        }

        public void Initialize(LevelInfo levelInfo)
        {
            // Process the grid in a single loop
            var gridObjectTypes = levelInfo.GridObjectTypes;
            var rows = gridObjectTypes.GetLength(0);
            var cols = gridObjectTypes.GetLength(1);

            List<BaseGridObject> gridObjects = new List<BaseGridObject>(100);
            _gridObjectFactoryHandler.PopulateGridWithObjects(gridObjectTypes, gridObjects);
            _gridModel.Initialize(gridObjects, cols, rows);
            // Set up match and effect handlers
            _matchHandler.Initialize(_gridModel.Grid);
            _boosterHandler.Initialize(_gridModel.Grid);
            _hintHandler.Initialize(_gridModel.Grid);
        }

        public void CompleteLevel()
        {
            //TODO:m_LevelModel.LevelIndex++;
            UTask.Wait(0.25f).Do(() => { _levelUIView.OpenSuccessPanel(); });
        }
        
        public void FailLevel()
        {
            UTask.Wait(0.25f).Do(() => { _levelUIView.OpenFailPanel(); });
        }

        // public void RequestLevel()
        // {
        //     void LevelEndActions()
        //     {
        //         m_UIView.CloseFailPanel();
        //         m_UIView.CloseSuccessPanel();
        //         _itemFactory.DestroyAllItems();
        //         _boosterFactory.DestroyAllBoosters();
        //         _obstacleFactory.DestroyAllObstacles();
        //         m_LevelModel.LoadLevel();
        //     }
        //
        //     m_LevelTransitionHandler.DoTransition(LevelEndActions);
        // }
    }
}