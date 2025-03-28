using _Drone_Attack.Configs.Sources;
using _Drone_Attack.Gameplay.Weapon.Scripts;
using _Drone_Attack.Infrastructure.Scripts.Services;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace _Drone_Attack.Gameplay.Enemy.Scripts
{
    [RequireComponent(typeof(NavMeshAgent), typeof(AudioSource))]
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _attackParticle;
        
        private EnemyConfigSource _enemyConfig;
        private PauseService _pauseService;
        private AbilitiesService _abilitiesService;

        private Transform _target;
        private NavMeshAgent _agent;
        private bool _isAttacking;
        private AudioSource _audioSource;

        [Inject]
        private void Constructor(PauseService pauseService, AbilitiesService abilitiesService)
        {
            _abilitiesService = abilitiesService;
            _pauseService = pauseService;
        }
        
        public void Setup(EnemyConfigSource enemyConfig, GameObject target)
        {
            _enemyConfig = enemyConfig;
            _target = target.transform;
            
            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = _enemyConfig.Speed;
            _agent.stoppingDistance = _enemyConfig.AttackDistance - 1;

            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if(_pauseService.IsPaused)
            {
                _agent.ResetPath();
                return;
            }

            _agent.speed = _enemyConfig.Speed * _abilitiesService.SpeedCoefficient;
            
            if(_target != null && gameObject.activeSelf)
            {
                _agent.SetDestination(_target.position);

                if (Vector3.Distance(_target.position, transform.position) <= _enemyConfig.AttackDistance)
                {
                    AttackTarget();
                }
            }
        }

        private void AttackTarget()
        {
            if (!_isAttacking && _target.GetComponent<IDamageable>() is {} damageable)
            {
                _isAttacking = true;
                damageable.TakeDamage(_enemyConfig.Damage);
                
                _attackParticle.Play();

                _audioSource.clip = _enemyConfig.AttackSound;
                _audioSource.Play();
                
                Invoke(nameof(CooldownAttackEnded), _enemyConfig.CooldownAttack);
            }
        }

        private void CooldownAttackEnded()
        {
            _isAttacking = false;
        }
    }
}
