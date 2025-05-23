using System.Collections.Generic;
using UnityEngine;

namespace _Drone_Attack.Configs.Sources
{
    [CreateAssetMenu(fileName = "WeaponsConfig", menuName = "Configs/WeaponsConfig")]
    public class WeaponsConfigSource : ScriptableObject
    {
        [SerializeField] private List<GameObject> _weapons;
        
        public List<GameObject> Weapons => _weapons;
    }
}
