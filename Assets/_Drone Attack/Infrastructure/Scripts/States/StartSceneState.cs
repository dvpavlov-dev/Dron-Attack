using _Drone_Attack.Infrastructure.Scripts.Services;
using _Drone_Attack.UI.Scripts;
using Shot_Shift.Infrastructure.Scripts;

namespace _Drone_Attack.Infrastructure.Scripts.States
{
    public class StartSceneState : IState
    {
        private readonly ILoadingCurtains _loadingCurtains;
        private readonly ISceneLoaderService _sceneLoaderService;

        public StartSceneState(ISceneLoaderService sceneLoaderService, ILoadingCurtains loadingCurtains)
        {
            _loadingCurtains = loadingCurtains;
            _sceneLoaderService = sceneLoaderService;
        }

        public void Enter()
        {
            _loadingCurtains.ShowLoadingCurtains("Loading game...");
            _sceneLoaderService.LoadScene("Start", OnLoadedScene);
        }
        
        public void Exit()
        {
            
        }
        
        private void OnLoadedScene()
        {
            _loadingCurtains.HideLoadingCurtains();
        }
    }
}