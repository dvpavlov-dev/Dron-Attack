using _Drone_Attack.Infrastructure.Scripts.Services;
using _Drone_Attack.Infrastructure.Scripts.States;
using Shot_Shift.Infrastructure.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Drone_Attack.UI.Scripts.GameLoopScene
{
    public class EndRoundWindow : WindowView
    {
        [SerializeField] private Image _backgroundTitleImage;
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private GameObject _rewardContainer;
        [SerializeField] private TMP_Text _rewardCoinsText;

        private GameStateMachine _gameStateMachine;
        private int _currentLevel;
        private Infrastructure.Scripts.Configs _configs;
        private PlayerProgressService _playerProgressService;

        [Inject]
        private void Constructor(GameStateMachine gameStateMachine, Infrastructure.Scripts.Configs configs, PlayerProgressService playerProgressService)
        {
            _playerProgressService = playerProgressService;
            _configs = configs;
            _gameStateMachine = gameStateMachine;
        }

        public void ShowWinningWindow()
        {
            _rewardContainer.SetActive(true);
            _rewardCoinsText.text = $"Reward coins: {_configs.LevelsConfig.Levels[_playerProgressService.CurrentLevel].RewardCoins}";
            
            _backgroundTitleImage.color = Color.green;
            _titleText.text = "You Win!";
        }

        public void ShowLoseWindow()
        {
            _rewardContainer.SetActive(false);
            _backgroundTitleImage.color = Color.red;
            _titleText.text = "You Lose!";
        }

        public void OnContinueSelected()
        {
            _gameStateMachine.Enter<GameLoopState>();
        }
        
        public void ReturnToStartScene()
        {
            _gameStateMachine.Enter<StartSceneState>();
        }
    }
}
