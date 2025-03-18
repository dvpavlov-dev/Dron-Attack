using _Drone_Attack.Infrastructure.Scripts.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Drone_Attack.UI.Scripts
{
    public class SettingsWindow : WindowView
    {
        [SerializeField] private Image _switcherSoundImage;
        [SerializeField] private TMP_Text _switcherSoundText;
        
        [SerializeField] private Image _switcherInvertImage;
        [SerializeField] private TMP_Text _switcherInvertText;
        
        private SettingsService _settingsService;

        [Inject]
        private void Constructor(SettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        private void OnEnable()
        {
            ShowSwitcherStatus(_settingsService.SoundStatus, ref _switcherSoundImage, ref _switcherSoundText);
        }

        public void ChangeSoundStatus()
        {
            bool value = !_settingsService.SoundStatus;
            
            ShowSwitcherStatus(value, ref _switcherSoundImage, ref _switcherSoundText);
            
            _settingsService.SoundStatus = value;
        }

        private void ShowSwitcherStatus(bool value, ref Image switcherImage, ref TMP_Text switcherText)
        {
            if (value)
            {
                switcherImage.color = Color.green;
                switcherText.text = "ON";
            }
            else
            {
                switcherImage.color = Color.red;
                switcherText.text = "OFF";
            }
        }
    }
}
