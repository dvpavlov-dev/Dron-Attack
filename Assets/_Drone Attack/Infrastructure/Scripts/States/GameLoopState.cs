using _Drone_Attack.Infrastructure.Scripts.Services;
using _Drone_Attack.UI.Scripts;
using Shot_Shift.Infrastructure.Scripts;

namespace _Drone_Attack.Infrastructure.Scripts.States
{
    public class GameLoopState : IState
    {
        private readonly ILevelProgressService _levelProgressService;
        private readonly ILoadingCurtains _loadingCurtains;
        private readonly ISceneLoaderService _sceneLoaderService;


        public GameLoopState(ILevelProgressService levelProgressService, 
            ILoadingCurtains loadingCurtains, 
            ISceneLoaderService sceneLoaderService)
        {
            _levelProgressService = levelProgressService;
            _loadingCurtains = loadingCurtains;
            _sceneLoaderService = sceneLoaderService;
        }

        public void Enter()
        {
            _loadingCurtains.ShowLoadingCurtains("Loading level...");
            _sceneLoaderService.LoadScene("GameLoop", OnLoadedScene);
        }
        
        public void Exit()
        {
            
        }

        private void OnLoadedScene()
        {
            _levelProgressService.LevelProgressWatcher.RunLevel();
        }
    }
}