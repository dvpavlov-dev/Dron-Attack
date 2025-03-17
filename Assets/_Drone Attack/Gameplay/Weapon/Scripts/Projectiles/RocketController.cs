using _Drone_Attack.Configs.Sources;
using UnityEngine;

namespace _Drone_Attack.Gameplay.Weapon.Scripts.Projectiles
{
    public class RocketController : ProjectileController<RocketConfigSource>
    {
        [SerializeField] private GameObject _explosionEffect;
        [SerializeField] private LayerMask _explosionLayer;
        
        protected override void CollideWithObject(Collider other)
        {
            if (!other.CompareTag("Player") && !other.CompareTag("Projectile"))
            {
                Debug.Log($"RocketController collided with {other.name}");
                Explosion();
                Instantiate(_hitSoundPrefab, transform.position, Quaternion.identity);
                Instantiate(_explosionEffect, transform.position, transform.rotation);
                Dispose();
            }
        }

        private void Explosion()
        {
            RaycastHit[] hits = new RaycastHit[100];
            int hitsCount = Physics.SphereCastNonAlloc(
                transform.position, 
                _projectileConfig.ExplosionRange, 
                transform.forward, 
                hits,
                _projectileConfig.ExplosionRange,
                _explosionLayer);
            
            if (hitsCount != 0)
            {
                for(int i = 0; i < hitsCount; i++)
                {
                    if (hits[i].transform.GetComponent<IDamageable>() is {} damageable)
                    {
                        if(hits[i].transform.CompareTag("Enemy"))
                        {
                            damageable.TakeDamage(_damage);
                        }

                        if (hits[i].transform.GetComponent<Rigidbody>() is {} rb)
                        {
                            rb.AddExplosionForce(_projectileConfig.ExplosionForce, transform.position, _projectileConfig.ExplosionRange);
                        }
                    }
                }
            }
        }
    }
}
