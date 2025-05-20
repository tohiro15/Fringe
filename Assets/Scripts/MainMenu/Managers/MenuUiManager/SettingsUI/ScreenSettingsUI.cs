using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ScreenSettingsUI : MonoBehaviour
{
    [SerializeField] private Button _windowModeButton;
    [SerializeField] private Image _windowModeEnableImage;
    [SerializeField] private TMP_Dropdown _resolutionDropdown;
    [SerializeField] private TMP_Dropdown _qualityDropdown;

    private ISettings _settings;

    public void Init(ISettings settings)
    {
        _settings = settings;

        PopulateResolutionDropdown();

        _windowModeEnableImage.gameObject.SetActive(_settings.GetIsWindowMode());
        _windowModeButton.onClick.AddListener(ChangeWindowMode);

        if (_qualityDropdown != null && _settings != null) _qualityDropdown.value = _settings.GetQualityLevel();
        _qualityDropdown?.onValueChanged.AddListener(_settings.ChangeQuality);

        if(_resolutionDropdown != null && _settings != null) _resolutionDropdown.value = _settings.GetResolution();
        _resolutionDropdown?.onValueChanged.AddListener(_settings.ChangeResolution);

    }
    private void ChangeWindowMode()
    {
        _settings.ChangeWindowMode();
        _windowModeEnableImage.gameObject.SetActive(_settings.GetIsWindowMode());
    }

    private void PopulateResolutionDropdown()
    {
        _resolutionDropdown?.ClearOptions();
        var options = new List<string>();
        foreach (var res in Screen.resolutions)
        {
            options.Add(res.width + " x " + res.height + " @ " + Mathf.RoundToInt((float)res.refreshRateRatio.value) + "Hz");
        }

        _resolutionDropdown?.AddOptions(options);
    }
}
