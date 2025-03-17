using UnityEngine;

namespace _Drone_Attack.Gameplay.Weapon.Scripts
{
    public class EffectsDestroyer : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;

        private void Start()
        {
            Destroy(gameObject, _particleSystem.main.duration);
        }
    }
}
