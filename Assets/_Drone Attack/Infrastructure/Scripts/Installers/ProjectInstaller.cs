using _Drone_Attack.UI.Scripts;
using UnityEngine;
using Zenject;

namespace _Drone_Attack.Infrastructure.Scripts.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private LoadingCurtains _loadingCurtains;
        
        public override void InstallBindings()
        {
            Container.Bind<ILoadingCurtains>().FromInstance(_loadingCurtains).AsSingle().NonLazy();
        }
    }
}