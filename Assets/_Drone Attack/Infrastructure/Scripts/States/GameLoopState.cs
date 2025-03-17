using _Drone_Attack.Infrastructure.Scripts.Factories;
using _Drone_Attack.Infrastructure.Scripts.Services;
using _Drone_Attack.UI.Scripts;
using R3;
using Shot_Shift.Infrastructure.Scripts;
using UnityEngine;

namespace _Drone_Attack.Infrastructure.Scripts.States
{
    public class GameLoopState : IState
    {
        private readonly ILevelProgressService _levelProgressService;
        private readonly ILoadingCurtains _loadingCurtains;
        private readonly ISceneLoaderService _sceneLoaderService;
        private readonly IActorsFactory _actorsFactory;
        private readonly IWeaponsFactory _weaponsFactory;

        public GameLoopState(ILevelProgressService levelProgressService, 
            ILoadingCurtains loadingCurtains, 
            ISceneLoaderService sceneLoaderService, 
            IActorsFactory actorsFactory,
            IWeaponsFactory weaponsFactory)
        {
            _levelProgressService = levelProgressService;
            _loadingCurtains = loadingCurtains;
            _sceneLoaderService = sceneLoaderService;
            _actorsFactory = actorsFactory;
            _weaponsFactory = weaponsFactory;
        }

        public void Enter()
        {
            _loadingCurtains.ShowLoadingCurtains("Loading level...");
            _sceneLoaderService.LoadScene("GameLoop", OnLoadedScene);
        }
        
        public void Exit()
        {
            
        }

        private void OnInitializedEnded()
        {
            _loadingCurtains.HideLoadingCurtains();
            _levelProgressService.LevelProgressWatcher.RunLevel();
        }

        private void OnLoadedScene()
        {
            _loadingCurtains.UpdateDescriptionText("Loading enemies...");
            _actorsFactory.InitializeFactory().Subscribe(_ =>
            {
                _loadingCurtains.UpdateDescriptionText("Loading bullets...");
                _weaponsFactory.InitializeFactory().Subscribe(_ =>
                {
                    Debug.Log("Метод 2 завершен!");
                    OnInitializedEnded();
                });
            });
        }
    }
}