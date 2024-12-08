using System.Collections.Generic;
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
        private readonly IBoosterFactory _boosterFactory;
        private readonly IGridModel _gridModel;
        private readonly MatchHandler _matchHandler;
        
        public LevelStateHandler(IItemFactory itemFactory, IBoosterFactory boosterFactory, IGridModel gridModel,
            MatchHandler matchHandler)
        {
            _itemFactory = itemFactory;
            _boosterFactory = boosterFactory;
            _gridModel = gridModel;
            _matchHandler = matchHandler;
        }

        public void Initialize(LevelInfo levelInfo)
        {
            List<BaseGridObject> gridObjects = new List<BaseGridObject>(64);
            var items = _itemFactory.GenerateItems(levelInfo.GridObjectTypes);
            gridObjects.AddRange(items);
            var boosters = _boosterFactory.GenerateBoosters(levelInfo.GridObjectTypes);
            gridObjects.AddRange(boosters);
            _gridModel.Initialize(
                gridObjects,
                levelInfo.GridObjectTypes.GetLength(1), 
                levelInfo.GridObjectTypes.GetLength(0)
            );

            // Set up match and effect handlers
            _matchHandler.Initialize(_gridModel.Grid);
            
            //m_BoosterHandler.Initialize(m_GridModel.Grid);
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
        //         m_LevelModel.LoadLevel();
        //     }
        //
        //     m_LevelTransitionHandler.DoTransition(LevelEndActions);
        // }
    }
}