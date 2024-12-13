using System;
using Cysharp.Threading.Tasks;
using DI;
using DI.Contexts;
using MVP.Models.Interface;
using MVP.Presenters.Handlers;
using MVP.Views.Interface;
using UnityEngine;

namespace MVP.Presenters
{
    public class LevelPresenter
    {
        private readonly LevelStateHandler _levelStateHandler;
        private readonly GoalHandler _goalHandler;
        private readonly IGridView _gridView;
        
        public LevelPresenter(LevelStateHandler levelStateHandler, GoalHandler goalHandler, IGridView gridView)
        {
            _levelStateHandler = levelStateHandler;
            _goalHandler = goalHandler;
            _gridView = gridView;
            
            _goalHandler.OnLevelCompleted += HandleLevelCompleted;
            _goalHandler.OnLevelFailed += HandleLevelFailed;
        }

        ~LevelPresenter()
        {
            _goalHandler.OnLevelCompleted -= HandleLevelCompleted;
            _goalHandler.OnLevelFailed -= HandleLevelFailed;
        }

        private void HandleLevelCompleted()
        {
            _levelStateHandler.CompleteLevel();
        }

        private void HandleLevelFailed()
        {
            _levelStateHandler.FailLevel();
        }

        public async UniTask LoadLevel()
        {
            var levelModel = ProjectContext.Container.Resolve<ILevelModel>();
            var levelInfo = levelModel.LoadLevel();//TODO:Make here adressables and wait that
            _gridView.CalculateGridSize(levelInfo.GridSize);
            _levelStateHandler.Initialize(levelInfo);
            _goalHandler.Initialize(levelInfo.Goals, levelInfo.NumberOfMoves);
            _gridView.ScaleGrid();
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
        }
        
    }
}
