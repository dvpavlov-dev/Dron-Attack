using _Drone_Attack.Infrastructure.Scripts.Services;
using UnityEngine;
using Zenject;

namespace _Drone_Attack.Gameplay.Drop.Scripts
{
    public class CoinsDropComponent : DropComponent
    {
        private PlayerProgressService _playerProgressService;
        
        [Inject]
        private void Constructor(PlayerProgressService playerProgressService)
        {
            _playerProgressService = playerProgressService;
        }
        
        public override void TakeDrop(GameObject actor)
        {
            if (actor.CompareTag("Player"))
            {
                _playerProgressService.AddCoins(10);
                Destroy(gameObject);
            }
        }
    }
}
