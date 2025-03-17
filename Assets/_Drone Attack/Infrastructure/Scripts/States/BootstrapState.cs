using Shot_Shift.Infrastructure.Scripts;

namespace _Drone_Attack.Infrastructure.Scripts.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _gameStateMachine;

        public BootstrapState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            _gameStateMachine.Enter<StartSceneState>();
        }
        
        public void Exit()
        {
        }
    }
}