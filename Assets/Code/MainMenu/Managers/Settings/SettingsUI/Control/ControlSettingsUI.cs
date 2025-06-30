using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlSettingsUI : MonoBehaviour
{
    [SerializeField] private Slider _sensitivitySlider;
    [SerializeField] private TMP_Text _sensitivityText;

    public void Init()
    {
        _sensitivitySlider.value = SettingsManager.Instance.GetSensitivity() * 10f;
        _sensitivitySlider.onValueChanged.AddListener(OnSliderValueChanged);

        UpdateControlLabel(_sensitivitySlider.value);
    }

    private void OnSliderValueChanged(float sliderValue)
    {
        float sensitivityNormalized = sliderValue / 10f;
        SettingsManager.Instance.ChangeSensitivity(sensitivityNormalized);
        UpdateControlLabel(sliderValue);
    }

    public void UpdateControlLabel(float sliderValue)
    {
        _sensitivityText.text = $"Чувствительность: {sliderValue:F0}";
    }
}
