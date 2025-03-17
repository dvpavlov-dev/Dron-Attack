using _Drone_Attack.Configs.Sources;
using UnityEngine;

namespace _Drone_Attack.Gameplay.Weapon.Scripts
{
    public class ShotgunController : WeaponController<ShotgunConfigSource>
    {
        protected override void SetupBullet()
        {
            for (int i = 0; i < _weaponConfig.NumberOfBilletsFired; i++)
            {
                GameObject bulletPref = _weaponsFactory.GetProjectile(_weaponConfig.ProjectileConfig);
                bulletPref.transform.position = _shootPoint.position;
                bulletPref.transform.rotation = _shootPoint.rotation;

                float startPoint = _weaponConfig.Spread * _weaponConfig.NumberOfBilletsFired / 2;
                bulletPref.transform.Rotate(new Vector3(0, -startPoint + _weaponConfig.Spread * i, 0));
                bulletPref.SetActive(true);
            }
        }
    }
}
