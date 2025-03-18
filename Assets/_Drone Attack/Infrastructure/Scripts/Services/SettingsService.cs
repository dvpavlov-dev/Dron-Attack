using System;
using UnityEngine;
using Zenject;

namespace _Drone_Attack.Infrastructure.Scripts.Services
{
    public class SettingsService : IInitializable
    {
        public bool SoundStatus
        {
            get => _soundStatus;
            set
            {
                _soundStatus = value;
                OnSoundStatusChanged?.Invoke();
                SaveSettings();
            }
        }

        public Action OnSoundStatusChanged;
        
        private bool _soundStatus;
        private bool _invertControlStatus;
        
        public void Initialize()
        {
            LoadSettings();
        }

        private void SaveSettings()
        {
            PlayerPrefs.SetInt("SOUND", SoundStatus ? 1 : 0);
            
            PlayerPrefs.Save();
        }

        private void LoadSettings()
        {
            SoundStatus = PlayerPrefs.GetInt("SOUND") == 1;
        }
    }
}
