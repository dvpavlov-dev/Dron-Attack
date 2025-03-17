using _Drone_Attack.Configs.Sources;
using _Drone_Attack.Infrastructure.Scripts.Factories;
using _Drone_Attack.Infrastructure.Scripts.Services;
using UnityEngine;
using Zenject;

namespace _Drone_Attack.Gameplay.Weapon.Scripts
{
    [RequireComponent(typeof(AudioSource))]
    public class WeaponController<TConfig> : MonoBehaviour, IWeaponController where TConfig : WeaponConfigSource
    {
        [SerializeField] private ParticleSystem _muzzleFlash;
        
        [SerializeField] protected TConfig _weaponConfig;
        [SerializeField] protected Transform _shootPoint;

        protected IWeaponsFactory _weaponsFactory;
        
        private AudioSource _audioSource;
        private PlayerProgressService _playerProgressService;
        private SettingsService _settingsService;

        public WeaponConfigSource WeaponConfig => _weaponConfig;

        [Inject]
        private void Constructor(IWeaponsFactory weaponsFactory, PlayerProgressService playerProgressService, SettingsService settingsService)
        {
            _settingsService = settingsService;
            _playerProgressService = playerProgressService;
            _weaponsFactory = weaponsFactory;
            
            _audioSource = GetComponent<AudioSource>();
        }
        
        public virtual Vector3 FireWithRecoil()
        {
            SetupBullet();

            if(_settingsService.SoundStatus)
            {
                _audioSource.clip = _weaponConfig.ShotSound;
                _audioSource.Play();
            }
            
            _muzzleFlash.Play();
            
            Vector3 recoilDirection = -_shootPoint.right;
            return recoilDirection * (_weaponConfig.RecoilForce * _playerProgressService.RecoilUpgrade);
        }
        
        protected virtual void SetupBullet()
        {
            GameObject projectilePref = _weaponsFactory.GetProjectile(_weaponConfig.ProjectileConfig);
            projectilePref.transform.position = _shootPoint.position;
            projectilePref.transform.rotation = _shootPoint.rotation;
            projectilePref.SetActive(true);
        }
    }

    public interface IWeaponController
    {
        Vector3 FireWithRecoil();
        WeaponConfigSource WeaponConfig { get; }
    }
}
