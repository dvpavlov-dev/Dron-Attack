using System;
using _Drone_Attack.Configs.Sources;
using _Drone_Attack.Gameplay.Weapon.Scripts;
using _Drone_Attack.Infrastructure.Scripts.Factories;
using R3;
using UnityEngine;
using Random = System.Random;

namespace _Drone_Attack.Infrastructure.Scripts.Services
{
    public class EnemySpawnerService
    {
        public Action LevelFinished { get; set; }

        private const float DISTANCE_SPAWN = 15;
        
        private IActorsFactory _actorsFactory;
        private Random _rnd = new();
        private int _totalEnemyCount = 0;
        private IDisposable _enemyObserver;

        public void SpawnEnemy(IActorsFactory actorsFactory, LevelsConfigSource.Level levelConfig, CompositeDisposable disposable)
        {
            _actorsFactory = actorsFactory;

            _totalEnemyCount = levelConfig.EnemyCount;
            int enemyCount = 0;
            
            _enemyObserver = Observable
                .Interval(TimeSpan.FromSeconds(0.5f))
                .Subscribe(_ =>
                {
                    GameObject enemy = _actorsFactory.GetEnemy();
                    enemy.transform.position = SetPositionEnemy();
                    enemy.GetComponent<IDamageable>().OnDeath += OnDeathEnemy;
                    enemyCount++;
                    
                    if(enemyCount >= levelConfig.EnemyCount)
                    {
                        _enemyObserver.Dispose();
                    }
                })
                .AddTo(disposable);
        }

        private void OnDeathEnemy(IDamageable enemyDamageable)
        {
            enemyDamageable.OnDeath -= OnDeathEnemy;
            
            if (--_totalEnemyCount == 0)
            {
                LevelFinished?.Invoke();
            }
        }
        
        private Vector3 SetPositionEnemy()
        {
            int rand = _rnd.Next(0, 360);
            float x = DISTANCE_SPAWN * Mathf.Cos(rand);
            float z = DISTANCE_SPAWN * Mathf.Sin(rand);
            return new Vector3(x, 1, z);
        }
    }
}
