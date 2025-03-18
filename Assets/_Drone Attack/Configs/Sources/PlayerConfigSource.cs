using UnityEngine;

namespace _Drone_Attack.Configs.Sources
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
    public class PlayerConfigSource : ScriptableObject
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private float _health;
        [SerializeField] private float _speed;
        
        public GameObject PlayerPrefab => _playerPrefab;
        public float Health => _health;
        public float Speed => _speed;
    }
}
