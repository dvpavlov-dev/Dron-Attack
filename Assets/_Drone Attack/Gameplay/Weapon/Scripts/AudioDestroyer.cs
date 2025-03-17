using UnityEngine;

namespace _Drone_Attack.Gameplay.Weapon.Scripts
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioDestroyer : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip[] _audioClips;
        
        public void Start()
        {
            _audioSource.clip = _audioClips[Random.Range(0, _audioClips.Length)];
            _audioSource.Play();
            
            Destroy(gameObject, _audioSource.clip.length);
        }
    }
}
