using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Core.Factories.Interface;
using Core.GridElements.GridPawns;
using Core.LevelSerialization;
using MVP.Models.Interface;
using UnityEngine;

namespace MVP.Presenters.Handlers
{
    public class LevelStateHandler
    {
        private readonly IItemFactory _itemFactory;
        private readonly IObstacleFactory _obstacleFactory;
        private readonly IBoosterFactory _boosterFactory;
        private readonly IGridModel _gridModel;
        private readonly HintHandler _hintHandler;
        private readonly BoosterHandler _boosterHandler;
        private readonly MatchHandler _matchHandler;
        
        public LevelStateHandler(IItemFactory itemFactory, IObstacleFactory obstacleFactory, IBoosterFactory boosterFactory, IGridModel gridModel,
            HintHandler hintHandler, BoosterHandler boosterHandler, MatchHandler matchHandler)
        {
            _itemFactory = itemFactory;
            _obstacleFactory = obstacleFactory;
            _boosterFactory = boosterFactory;
            _gridModel = gridModel;
            _hintHandler = hintHandler;
            _boosterHandler = boosterHandler;
            _matchHandler = matchHandler;
        }

        public void Initialize(LevelInfo levelInfo)
        {
            List<BaseGridObject> gridObjects = new List<BaseGridObject>(100);
            var items = _itemFactory.GenerateItems(levelInfo.GridObjectTypes);
            gridObjects.AddRange(items);
            var obstacles = _obstacleFactory.GenerateObstacles(levelInfo.GridObjectTypes);
            gridObjects.AddRange(obstacles);
            var boosters = _boosterFactory.GenerateBoosters(levelInfo.GridObjectTypes);
            gridObjects.AddRange(boosters);
            gridObjects = gridObjects.OrderBy(obj => obj.Coordinate.x).ThenBy(obj => obj.Coordinate.y).ToList();//TODO:
            
            _gridModel.Initialize(
                gridObjects,
                levelInfo.GridObjectTypes.GetLength(1), 
                levelInfo.GridObjectTypes.GetLength(0)
            );

            // Set up match and effect handlers
            _matchHandler.Initialize(_gridModel.Grid);
            _boosterHandler.Initialize(_gridModel.Grid);
            _hintHandler.Initialize(_gridModel.Grid);
            
        }

        // public void CompleteLevel()
        // {
        //     m_LevelModel.LevelIndex++;
        //     UTask.Wait(0.25f).Do(() => { m_UIView.OpenSuccessPanel(); });
        // }
        //
        // public void FailLevel()
        // {
        //     UTask.Wait(0.25f).Do(() => { m_UIView.OpenFailPanel(); });
        // }

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