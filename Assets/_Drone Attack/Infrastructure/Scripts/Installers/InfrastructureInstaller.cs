using _Drone_Attack.Infrastructure.Scripts.Factories;
using _Drone_Attack.Infrastructure.Scripts.Services;
using UnityEngine;
using Zenject;

namespace _Drone_Attack.Infrastructure.Scripts.Installers
{
    public class InfrastructureInstaller : MonoInstaller
    {
        [SerializeField] private Configs _configs;
    
        public override void InstallBindings()
        {
            Container.Bind<Configs>().FromInstance(_configs).AsSingle().NonLazy();
        
            BindServices();
            BindFactories();
        }
    
        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<SettingsService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AbilitiesService>().AsSingle().NonLazy();
            Container.Bind<PauseService>().AsSingle().NonLazy();
            Container.BindInterfacesTo<WeaponService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerProgressService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<LevelProgressServiceResolver>()
                .AsSingle()
                .CopyIntoDirectSubContainers();
            Container.BindInterfacesAndSelfTo<LevelProgressService>().AsSingle().NonLazy();
            Container.BindInterfacesTo<SceneLoaderService>().AsSingle().NonLazy();
            BindInputService();
        }

        private void BindFactories()
        {
            Container.BindInterfacesAndSelfTo<StateFactory>().AsSingle();
            Container.BindInterfacesTo<ActorsFactory>().AsSingle().NonLazy();
            Container.BindInterfacesTo<WeaponsFactory>().AsSingle().NonLazy();
            Container.BindInterfacesTo<DropFactory>().AsSingle().NonLazy();
        }
    
        private void BindInputService()
        {
        #if UNITY_STANDALONE
            Container.Bind<IInputService>().FromInstance(new StandaloneInputService()).AsSingle().NonLazy();
        #elif UNITY_ANDROID
            Container.Bind<IInputService>().FromInstance(new MobileInputService()).AsSingle().NonLazy();
        #endif
        }
    }
}
