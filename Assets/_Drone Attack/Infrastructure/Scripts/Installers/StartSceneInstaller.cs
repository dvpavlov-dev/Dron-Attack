using _Drone_Attack.UI.Scripts.StartScene;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _Drone_Attack.Infrastructure.Scripts.Installers
{
    public class StartSceneInstaller : MonoInstaller
    {
        [FormerlySerializedAs("_startMenuController")]
        [SerializeField] private StartMenuUIController _startMenuUIController;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_startMenuUIController).AsSingle().NonLazy();
        }
    }
}
