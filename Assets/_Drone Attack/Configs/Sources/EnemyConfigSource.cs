﻿using UnityEngine;

namespace _Drone_Attack.Configs.Sources
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/EnemyConfig")]
    public class EnemyConfigSource : ScriptableObject
    {
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private float _health;
        [SerializeField] private float _damage;
        [SerializeField] private float _cooldownAttack;
        [SerializeField] private float _attackDistance;
        [SerializeField] private float _speed;
        [SerializeField] private AudioClip _attackSound;

        public GameObject EnemyPrefab => _enemyPrefab;
        public float Health => _health;
        public float Damage => _damage;
        public float CooldownAttack => _cooldownAttack;
        public float AttackDistance => _attackDistance;
        public float Speed => _speed;
        public AudioClip AttackSound => _attackSound;
    }
}
