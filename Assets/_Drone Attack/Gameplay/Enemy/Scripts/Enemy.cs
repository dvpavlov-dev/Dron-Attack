using _Drone_Attack.Configs.Sources;
using _Drone_Attack.Gameplay.Weapon.Scripts;
using _Drone_Attack.Infrastructure.Scripts.Factories;
using _Drone_Attack.UI.Scripts.GameLoopScene;
using UnityEngine;
using Zenject;

namespace _Drone_Attack.Gameplay.Enemy.Scripts
{
    [RequireComponent(typeof(DamageController), typeof(EnemyAI))]
    public class Enemy : MonoBehaviour, IEnemy
    {
        [SerializeField] private GameObject _deadEffectPrefab;
        [SerializeField] private GameObject _deadSoundPrefab;
        [SerializeField] private HealthBarView _healthBarView;
        
        private IDamageable _damageController;
        private EnemyAI _enemyAI;
        private IActorsFactory _actorsFactory;
        private IDropFactory _dropFactory;

        [Inject]
        private void Constructor(IActorsFactory actorsFactory, IDropFactory dropFactory)
        {
            _dropFactory = dropFactory;
            _actorsFactory = actorsFactory;
        }
        
        public void Setup(EnemyConfigSource enemyConfig, GameObject target)
        {
            _damageController = GetComponent<DamageController>();
            _enemyAI = GetComponent<EnemyAI>();

            _healthBarView.SetupHealthBar(enemyConfig.Health, true);
            _damageController.Setup(enemyConfig.Health);
            _damageController.OnHealthChanged += _healthBarView.UpdateHealthBar;
            _damageController.OnDeath += Dispose;
            
            _enemyAI.Setup(enemyConfig, target);
        }
        
        private void Dispose(IDamageable enemyDamageable)
        {
            enemyDamageable.OnHealthChanged -= _healthBarView.UpdateHealthBar;
            enemyDamageable.OnDeath -= Dispose;

            GameObject drop = _dropFactory.CreateDrop(Random.Range(0, 2) == 0 ? DropType.MEDKIT : DropType.COINS);
            drop.transform.position = transform.position;
            
            Instantiate(_deadSoundPrefab, transform.position, Quaternion.identity);
            Instantiate(_deadEffectPrefab, transform.position, Quaternion.identity);
            
            _actorsFactory.DisposeEnemy(gameObject);
        }
    }
    
    public interface IEnemy
    {
        void Setup(EnemyConfigSource enemyConfig, GameObject target);
    }
}
