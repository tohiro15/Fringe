using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ScreenSettingsUI : MonoBehaviour
{
    [SerializeField] private Button _windowModeButton;
    [SerializeField] private Image _windowModeEnableImage;
    [SerializeField] private TMP_Dropdown _resolutionDropdown;

    public void Init()
    {

        PopulateResolutionDropdown();

        _windowModeEnableImage.gameObject.SetActive(SettingsManager.Instance.GetIsWindowMode());
        _windowModeButton.onClick.AddListener(ChangeWindowMode);

        if(_resolutionDropdown != null) _resolutionDropdown.value = SettingsManager.Instance.GetResolution();
        _resolutionDropdown?.onValueChanged.AddListener(SettingsManager.Instance.ChangeResolution);

    }
    private void ChangeWindowMode()
    {
        SettingsManager.Instance.ChangeWindowMode();
        _windowModeEnableImage.gameObject.SetActive(SettingsManager.Instance.GetIsWindowMode());
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
