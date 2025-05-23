using System;
using _Drone_Attack.Configs.Sources;
using _Drone_Attack.Gameplay.Weapon.Scripts;
using _Drone_Attack.Infrastructure.Scripts.Factories;
using _Drone_Attack.Infrastructure.Scripts.Services;
using _Drone_Attack.UI.Scripts;
using _Drone_Attack.UI.Scripts.GameLoopScene;
using R3;
using UnityEngine;
using Zenject;

namespace _Drone_Attack.Infrastructure.Scripts
{
    public class LevelProgressWatcher : MonoBehaviour
    {
        [SerializeField] private GameLoopUIController _gameLoopUIController;
        
        private EnemySpawnerService _enemySpawnerService = new();
        private IActorsFactory _actorsFactory;
        private Configs _configs;
        private PlayerProgressService _playerProgressService;
        private PauseService _pauseService;

        private CompositeDisposable _disposable = new();
        private IDamageable _playerDamageable;
        private IDisposable _timerObserver;
        private ILoadingCurtains _loadingCurtains;
        private IWeaponsFactory _weaponsFactory;

        [Inject]
        private void Construct(
            IActorsFactory actorsFactory, 
            Configs configs, 
            PlayerProgressService playerProgressService, 
            PauseService pauseService,
            ILoadingCurtains loadingCurtains,
            IWeaponsFactory weaponsFactory)
        {
            _weaponsFactory = weaponsFactory;
            _loadingCurtains = loadingCurtains;
            _pauseService = pauseService;
            _playerProgressService = playerProgressService;
            _configs = configs;
            _actorsFactory = actorsFactory;
        }

        public void RunLevel()
        {
            Debug.Log("LevelProgressWatcher.RunLevel");
            
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

        private void OnInitializedEnded()
        {
            _loadingCurtains.HideLoadingCurtains();
            
            LevelsConfigSource.Level currentLevelConfig = _configs.LevelsConfig.Levels[_playerProgressService.CurrentLevel];
            _pauseService.IsPaused = false;
            
            HudSetup(currentLevelConfig);
            PlayerSetup();
            EnemySpawnerSetup();
        }
        
        private void HudSetup(LevelsConfigSource.Level currentLevelConfig)
        {
            _gameLoopUIController.HudView.SetupHud(
                _configs.PlayerConfig.Health * _playerProgressService.HealthUpgrade, 
                currentLevelConfig.IsTimerNeeded ? currentLevelConfig.TimerIntervalInSeconds : 0);
            
            _playerProgressService.OnCoinsChanged += UpdateCoins;
            UpdateCoins();

            if (currentLevelConfig.IsTimerNeeded)
            {
                StartTimer(currentLevelConfig.TimerIntervalInSeconds);
            }
        }
        
        private void EnemySpawnerSetup()
        {
            _enemySpawnerService.SpawnEnemy(_actorsFactory, _configs.LevelsConfig.Levels[_playerProgressService.CurrentLevel], _disposable);
            _enemySpawnerService.LevelFinished += OnLevelFinished;
        }
        
        private void PlayerSetup()
        {
            GameObject player = _actorsFactory.CreatePlayer();
            _playerDamageable = player.GetComponent<IDamageable>();
            _playerDamageable.OnDeath += OnLevelLost;
            _playerDamageable.OnHealthChanged += OnPlayerHealthChanged;
        }

        private void StartTimer(int startSeconds)
        {
            _timerObserver = Observable
                .Interval(TimeSpan.FromSeconds(1))
                .Subscribe(_ =>
                {
                    if(--startSeconds > 0)
                    {
                        UpdateTimer(startSeconds);
                    }
                    else
                    {
                        UpdateTimer(0);
                        OnLevelLost(_playerDamageable);
                        _timerObserver.Dispose();
                    }
                })
                .AddTo(_disposable);
        }

        private void OnPlayerHealthChanged(float health)
        {
            _gameLoopUIController.HudView.UpdateHealth(health);
        }

        private void UpdateTimer(int seconds)
        {
            _gameLoopUIController.HudView.UpdateTimer(seconds);
        }

        private void UpdateCoins()
        {
            _gameLoopUIController.HudView.UpdateCoins(_playerProgressService.CoinsCount);
        }
        
        private void OnLevelLost(IDamageable playerDamageable)
        {
            Debug.Log("LevelProgressWatcher.LevelLost");
            
            playerDamageable.OnDeath -= OnLevelLost;
            playerDamageable.OnHealthChanged -= OnPlayerHealthChanged;
            _gameLoopUIController.ShowLoseWindow();
            _pauseService.IsPaused = true;
        }

        private void OnLevelFinished()
        {
            Debug.Log("LevelProgressWatcher.LevelFinished");
            
            _enemySpawnerService.LevelFinished -= OnLevelFinished;
            _playerProgressService.ChangeLevelData(_playerProgressService.CurrentLevel + 1);
            _playerProgressService.AddCoins(_configs.LevelsConfig.Levels[_playerProgressService.CurrentLevel].RewardCoins);
            
            _gameLoopUIController.ShowWinningWindow();
            _pauseService.IsPaused = true;
        }

        private void OnDestroy()
        {
            _playerProgressService.OnCoinsChanged -= UpdateCoins;
            
            _timerObserver?.Dispose();
            _disposable.Dispose();
        }
    }
}
