using _Drone_Attack.Gameplay.Weapon.Scripts;
using UnityEngine;

namespace _Drone_Attack.Gameplay.Drop.Scripts
{
    public class MedkitDropComponent : DropComponent
    {
        public override void TakeDrop(GameObject actor)
        {
            if (actor.CompareTag("Player") && actor.GetComponent<IDamageable>() is {} damageable)
            {
                damageable.TakeHealing(10);
                Destroy(gameObject);
            }
        }
    }
}
