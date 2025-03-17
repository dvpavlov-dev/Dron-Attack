using UnityEngine;
using Zenject;

namespace _Drone_Attack.Infrastructure.Scripts.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private LevelProgressWatcher levelProgressWatcher;
        
        public override void InstallBindings()
        {
            Container.BindInstance(levelProgressWatcher).AsSingle().NonLazy();
        }
    }
}
