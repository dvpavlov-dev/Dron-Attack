using UnityEngine;

namespace _Drone_Attack.Configs.Sources
{
    [CreateAssetMenu(fileName = "AbilitiesConfig", menuName = "Configs/AbilitiesConfig")]
    public class AbilitiesConfigSource : ScriptableObject
    {
        [SerializeField] private BulletTimeConfigSource _bulletTimeConfig;
        
        public BulletTimeConfigSource BulletTimeConfig => _bulletTimeConfig;
    }
}
