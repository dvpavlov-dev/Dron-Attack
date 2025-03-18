using _Drone_Attack.Infrastructure.Scripts.Services;
using UnityEngine;
using Zenject;

namespace _Drone_Attack.Gameplay.Player.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovementComponent : MonoBehaviour
    {
        private IInputService _inputService;
        private PauseService _pauseService;
        
        private Vector3 _rotatePlayerTo;
        private Camera _camera;
        private Vector3 _movement;
        private Rigidbody _rb;
        private bool _isInitialized;
        private Infrastructure.Scripts.Configs _configs;

        [Inject]
        private void Constructor(IInputService inputService, PauseService pauseService, Infrastructure.Scripts.Configs configs)
        {
            _configs = configs;
            _pauseService = pauseService;
            _inputService = inputService;
        }

        public void Setup()
        {
            _camera = Camera.main;
            _rb = GetComponent<Rigidbody>();
            
            _isInitialized = true;
        }

        private void Update()
        {
            if(!_isInitialized || _pauseService.IsPaused)
                return;
            
            RotatePlayer();
            CalcMove();
        }

        private void RotatePlayer()
        {
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (groundPlane.Raycast(ray, out float position))
            {
                Vector3 worldPosition = ray.GetPoint(position);
                _rotatePlayerTo.Set(worldPosition.x, worldPosition.y + 0.5f, worldPosition.z);
                transform.rotation = LookAt2D_Y(_rotatePlayerTo);
            }
        }

        private Quaternion LookAt2D_Y(Vector3 target)
        {
            var rotation = Quaternion.LookRotation(target - transform.position, transform.TransformDirection(Vector3.up));
            return new Quaternion(0, rotation.y, 0, rotation.w);
        }

        private void CalcMove()
        {
            float deltaX = _inputService.MoveAxis.x;
            float deltaZ = _inputService.MoveAxis.y;
            
            Vector3 move = new(deltaX, 0, deltaZ);
            move.Normalize();

            _movement.Set(_configs.PlayerConfig.Speed * move.x, 0, _configs.PlayerConfig.Speed * move.z);
            _rb.linearVelocity = _movement;
        }
    }
}
