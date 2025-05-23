using TMPro;
using UnityEngine;

public class QualitySettingsUI : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _qualityDropdown;

    private ISettings _settings;
    public void Init(ISettings settings)
    {
        _settings = settings;

        if (_qualityDropdown != null && _settings != null) _qualityDropdown.value = _settings.GetQualityLevel();
        _qualityDropdown?.onValueChanged.AddListener(_settings.ChangeQuality);
    }
}
