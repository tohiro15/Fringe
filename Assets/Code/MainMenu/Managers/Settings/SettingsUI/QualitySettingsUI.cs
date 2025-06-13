using TMPro;
using UnityEngine;

public class QualitySettingsUI : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _qualityDropdown;
    public void Init()
    {
        if (_qualityDropdown != null) _qualityDropdown.value = SettingsManager.Instance.GetQualityLevel();
        _qualityDropdown?.onValueChanged.AddListener(SettingsManager.Instance.ChangeQuality);
    }
}
