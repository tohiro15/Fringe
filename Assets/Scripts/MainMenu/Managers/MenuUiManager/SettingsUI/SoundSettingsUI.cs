using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoundSettingsUI : MonoBehaviour
{
    [SerializeField] private Slider _soundSlider;
    [SerializeField] private TMP_Text _volumeText;

    private ISettings _settings;

    public void Init(ISettings settings)
    {
        _settings = settings;

        _soundSlider.value = _settings.GetVolume();
        _soundSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    private void OnVolumeChanged(float volume)
    {
        _settings.ChangeVolume(volume);
        _volumeText.text = $"Общая Громкость: {volume:F0}";
    }
}
