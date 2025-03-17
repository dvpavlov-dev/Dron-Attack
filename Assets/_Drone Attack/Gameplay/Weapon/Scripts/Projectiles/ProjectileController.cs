using _Drone_Attack.Configs.Sources;
using _Drone_Attack.Infrastructure.Scripts.Factories;
using _Drone_Attack.Infrastructure.Scripts.Services;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _Drone_Attack.Gameplay.Weapon.Scripts.Projectiles
{
    public class ProjectileController<TProjectileConfig> : MonoBehaviour where TProjectileConfig : ProjectileConfigSource
    {
        [FormerlySerializedAs("_bulletConfig")]
        [SerializeField] protected TProjectileConfig _projectileConfig;
        [SerializeField] protected GameObject _hitSoundPrefab;
        [SerializeField] private GameObject _hitEffectPrefab;
        
        private IWeaponsFactory _weaponsFactory;
        private AbilitiesService _abilitiesService;
        
        protected float _damage;
        protected float _speed;
        protected float _range;

        private Vector3 _endPoint;
        private PlayerProgressService _playerProgressService;

        [Inject]
        private void Constructor(IWeaponsFactory weaponsFactory, AbilitiesService abilitiesService, PlayerProgressService playerProgressService)
        {
            _playerProgressService = playerProgressService;
            _abilitiesService = abilitiesService;
            _weaponsFactory = weaponsFactory;
        }

        private void OnEnable()
        {
            Setup(_projectileConfig.Damage * _playerProgressService.DamageUpgrade, _projectileConfig.Speed, _projectileConfig.Range);
        }

        protected virtual void CollideWithObject(Collider other)
        {
            if(!other.CompareTag("Projectile"))
            {
                if (!other.CompareTag("Player"))
                {
                    if (other.gameObject.GetComponent<IDamageable>() is {} damageable)
                    {
                        damageable.TakeDamage(_damage);
                        Instantiate(_hitSoundPrefab, transform.position, Quaternion.identity);
                    }

                    Dispose();
                }

                CreateHitEffect(other);
            }
        }
        
        private void CreateHitEffect(Collider other)
        {
            GameObject hitEffect = Instantiate(_hitEffectPrefab, transform.position, Quaternion.identity);
            Vector3 hitPoint = other.ClosestPointOnBounds(transform.position);
            Vector3 normal = (hitPoint - transform.position).normalized;
            hitEffect.transform.rotation = Quaternion.LookRotation(normal);
        }

        protected void Dispose()
        {
            _weaponsFactory.DisposeProjectile(_projectileConfig, gameObject);
        }

        private void Setup(float damage, float speed, float range)
        {
            _damage = damage;
            _speed = speed;
            _range = range;

            _endPoint = transform.position + transform.right * _range;
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, _endPoint, Time.deltaTime * _speed * _abilitiesService.SpeedCoefficient);
            
            if (Vector3.Distance(transform.position, _endPoint) < 1f)
            {
                Dispose();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            CollideWithObject(other);
        }
    }
}
