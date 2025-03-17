using _Drone_Attack.Infrastructure.Scripts.States;
using Shot_Shift.Infrastructure.Scripts;
using Zenject;

namespace _Drone_Attack.UI.Scripts.GameLoopScene
{
    public class MenuWindow : WindowView
    {
        private GameStateMachine _gameStateMachine;
        
        [Inject]
        private void Constructor(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void ReturnToStartScene()
        {
            _gameStateMachine.Enter<StartSceneState>();
        }
    }
}
