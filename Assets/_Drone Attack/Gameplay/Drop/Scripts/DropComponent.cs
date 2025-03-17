using UnityEngine;

namespace _Drone_Attack.Gameplay.Drop.Scripts
{
    public abstract class DropComponent : MonoBehaviour
    {
        public abstract void TakeDrop(GameObject actor);

        private void OnTriggerEnter(Collider other)
        {
            TakeDrop(other.gameObject);
        }
    }
}
