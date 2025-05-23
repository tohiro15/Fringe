using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SettingsSection
{
    public SettingsCategory Category;
    public Button Button;
    public TMP_Text Label;
    public GameObject Panel;
    public string LabelName;
}