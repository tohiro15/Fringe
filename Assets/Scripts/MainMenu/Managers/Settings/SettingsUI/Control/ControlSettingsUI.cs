using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlSettingsUI : MonoBehaviour
{
    [SerializeField] private Slider _sensitivitySlider;
    [SerializeField] private TMP_Text _sensitivityText;

    private ISettings _settings;

    public void Init(ISettings settings)
    {
        _settings = settings;

        _sensitivitySlider.value = _settings.GetSensitivity() * 10f;
        _sensitivitySlider.onValueChanged.AddListener(OnSliderValueChanged);

        UpdateControlLabel(_sensitivitySlider.value);
    }

    private void OnSliderValueChanged(float sliderValue)
    {
        float sensitivityNormalized = sliderValue / 10f;
        _settings.ChangeSensitivity(sensitivityNormalized);
        UpdateControlLabel(sliderValue);
    }

    private void UpdateControlLabel(float sliderValue)
    {
        _sensitivityText.text = $"Чувствительность: {sliderValue:F0}";
    }
}
