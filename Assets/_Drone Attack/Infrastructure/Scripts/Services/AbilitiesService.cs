using System;
using _Drone_Attack.Configs.Sources;
using R3;
using Zenject;

namespace _Drone_Attack.Infrastructure.Scripts.Services
{
    public class AbilitiesService : IDisposable
    {
        public float SpeedCoefficient { get; set; } = 1;

        private Configs _config;
        private PlayerProgressService _playerProgressService;
        private BulletTimeConfigSource _bulletTimeConfig;

        private IDisposable _startTimer;

        [Inject]
        private void Constructor(Configs config, PlayerProgressService playerProgressService)
        {
            _playerProgressService = playerProgressService;
            _config = config;
            
            _bulletTimeConfig = _config.AbilitiesConfig.BulletTimeConfig;
        }
        
        public void ActivateBulletTime()
        {
            if (SpeedCoefficient >= 1 && _playerProgressService.TryReduceCoinsData(_bulletTimeConfig.Cost))
            {
                SpeedCoefficient = 1 - _bulletTimeConfig.SlowdownInPercent / 100;

                _startTimer = Observable
                    .Timer(TimeSpan.FromSeconds(_bulletTimeConfig.DurationSlowdownInSeconds))
                    .Subscribe(_ =>
                    {
                        SpeedCoefficient = 1;
                        _startTimer.Dispose();
                    });
            }
        }
        
        public void Dispose()
        {
            _startTimer?.Dispose();
        }
    }
}
