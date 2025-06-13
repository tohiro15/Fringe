using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoundSettingsUI : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _volumeText;

    [SerializeField] private SoundType _soundType;

    public void Init()
    {

        switch (_soundType)
        {
            case SoundType.SFX:
                _slider.value = SettingsManager.Instance.GetSFXVolume();
                _slider.onValueChanged.AddListener(OnSFXVolumeChanged);
                break;
            case SoundType.Music:
                _slider.value = SettingsManager.Instance.GetMusicVolume();
                _slider.onValueChanged.AddListener(OnMusicVolumeChanged);
                break;
        }

        UpdateVolumeLabel(_slider.value);
    }

    private void OnSFXVolumeChanged(float value)
    {
        SettingsManager.Instance.ChangeSFXVolume(value);
        UpdateVolumeLabel(value);
    }

    private void OnMusicVolumeChanged(float value)
    {
        SettingsManager.Instance.ChangeMusicVolume(value);
        UpdateVolumeLabel(value);
    }

    private void UpdateVolumeLabel(float value)
    {
        if (_soundType == SoundType.SFX)
        {
            _volumeText.text = $"Громкость эффектов: {value:F0}%";
        }
        else
        {
            _volumeText.text = $"Громкость музыки: {value:F0}%";
        }
    }
}
