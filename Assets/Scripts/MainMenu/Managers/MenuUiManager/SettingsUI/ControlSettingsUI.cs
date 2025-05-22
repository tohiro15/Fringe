using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.Rendering.DebugUI;

public class ControlSettingsUI : MonoBehaviour
{
    [SerializeField] private Slider _sensitivitySlider;
    [SerializeField] private TMP_Text _sensitivityText;

    private ISettings _settings;

    public void Init(ISettings settings)
    {
        _settings = settings;

        _sensitivitySlider.value = _settings.GetSensitivity();
        _sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);

        UpdateControlLabel(_settings.GetSensitivity());
    }

    private void OnSensitivityChanged(float value)
    {
        _settings.ChangeSensitivity(value);
        UpdateControlLabel(value);
    }

    private void UpdateControlLabel(float value)
    {
        _sensitivityText.text = $"Чувствительность: {value:F0}";
    }
}
