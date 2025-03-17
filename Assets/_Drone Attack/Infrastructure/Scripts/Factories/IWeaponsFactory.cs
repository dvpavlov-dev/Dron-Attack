using _Drone_Attack.Configs.Sources;
using _Drone_Attack.Gameplay.Weapon.Scripts;
using R3;
using UnityEngine;

namespace _Drone_Attack.Infrastructure.Scripts.Factories
{
    public interface IWeaponsFactory
    {
        Observable<Unit> InitializeFactory();
        void CreateWeapons(Transform weaponSpot);
        IWeaponController GetFirstWeapon();
        IWeaponController GetNextWeapon();
        GameObject GetProjectile(ProjectileConfigSource projectilePrefab);
        void DisposeProjectile(ProjectileConfigSource projectileConfig, GameObject projectile);
    }
}