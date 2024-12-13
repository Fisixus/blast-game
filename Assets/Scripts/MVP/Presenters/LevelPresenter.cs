using System;
using Cysharp.Threading.Tasks;
using DI;
using DI.Contexts;
using MVP.Models.Interface;
using MVP.Presenters.Handlers;
using MVP.Views.Interface;
using UnityEngine;
using UTasks;

namespace MVP.Presenters
{
    public class LevelPresenter
    {
        private readonly LevelSetupHandler _levelSetupHandler;
        private readonly GoalHandler _goalHandler;
        private readonly IGridView _gridView;
        private readonly ILevelModel _levelModel;
        private readonly ILevelUIView _levelUIView;
        
        public LevelPresenter(LevelSetupHandler levelSetupHandler, GoalHandler goalHandler, IGridView gridView, ILevelUIView levelUIView)
        {
            _levelSetupHandler = levelSetupHandler;
            _goalHandler = goalHandler;
            _gridView = gridView;
            _levelUIView = levelUIView;
            _levelModel = ProjectContext.Container.Resolve<ILevelModel>();

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
            _levelModel.LevelIndex++;
            UTask.Wait(0.25f).Do(() => { _levelUIView.OpenSuccessPanel(); });
        }

        private void HandleLevelFailed()
        {
            UTask.Wait(0.25f).Do(() => { _levelUIView.OpenFailPanel(); });
        }

        public async UniTask LoadLevel()
        {
            var levelModel = ProjectContext.Container.Resolve<ILevelModel>();
            var levelInfo = levelModel.LoadLevel();//TODO:Make here adressables and wait that
            _gridView.CalculateGridSize(levelInfo.GridSize);
            _levelSetupHandler.Initialize(levelInfo);
            _goalHandler.Initialize(levelInfo.Goals, levelInfo.NumberOfMoves);
            _gridView.ScaleGrid();
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
        }
        
    }
}
