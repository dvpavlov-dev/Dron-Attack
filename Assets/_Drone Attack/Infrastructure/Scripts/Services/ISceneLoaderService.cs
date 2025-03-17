using System;

namespace _Drone_Attack.Infrastructure.Scripts.Services
{
    public interface ISceneLoaderService
    {
        void LoadScene(string sceneName, Action onSceneLoaded = null);
    }
}