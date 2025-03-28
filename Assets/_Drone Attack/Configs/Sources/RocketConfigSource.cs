﻿using UnityEngine;

namespace _Drone_Attack.Configs.Sources
{
    [CreateAssetMenu(fileName = "RocketConfig", menuName = "Configs/Bullets/Rocket Config")]
    public class RocketConfigSource : ProjectileConfigSource
    {
        [SerializeField] private float _explosionRange;
        [SerializeField] private float _explosionForce;

        public float ExplosionRange => _explosionRange;
        public float ExplosionForce => _explosionForce;
    }
}
