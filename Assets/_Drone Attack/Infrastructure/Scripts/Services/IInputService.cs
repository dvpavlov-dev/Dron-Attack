using UnityEngine;

namespace _Drone_Attack.Infrastructure.Scripts.Services
{
    public interface IInputService
    {
        public Vector2 MoveAxis { get; }
        public Vector2 RotateAxis { get; }
        public bool Interact { get; }
        public bool SwitchWeapon { get; }
        bool UseAbility { get; }
    }
}