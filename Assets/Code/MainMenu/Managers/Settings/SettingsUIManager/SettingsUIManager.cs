using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUIManager : MonoBehaviour, ISettingsUI
{
    public static SettingsUIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    [Header("Panels")]

    [SerializeField] private GameObject _confirmPanel;

    [Header("Buttons")]

    [Header("Settings")]

    [SerializeField] private Button _applySettingsButton;

    [Header("Confirm Panel")]

    [SerializeField] private Button _confirmButton;
    [SerializeField] private Button _revertButton;

    [Header("Settings")]
    [Space]


    [Header("Controllers")]

    [SerializeField] private ScreenSettingsUI _screenSettingsUI;
    [SerializeField] private QualitySettingsUI _qualitySettingsUI;
    [SerializeField] private SoundSettingsUI[] _soundSettingsUI;
    [SerializeField] private ControlSettingsUI _controlSettingsUI;

    [Header("Settings Sections")]

    [SerializeField] private List<SettingsSection> _settingsSections;

    private SettingsCategory _currentCategory;

    public void Init()
    {
        _screenSettingsUI?.Init();
        _qualitySettingsUI?.Init();
        _controlSettingsUI?.Init();

        foreach (var soundSettings in _soundSettingsUI)
        {
            soundSettings.Init();
        }

        foreach (var section in _settingsSections)
        {
            SettingsCategory capturedCategory = section.Category;
            section.Button?.onClick.AddListener(() => OpenSettings(capturedCategory));
        }

        _confirmPanel?.gameObject.SetActive(false);

        _applySettingsButton?.onClick.RemoveAllListeners();
        _applySettingsButton?.onClick.AddListener(ApplySettings);

        _revertButton?.onClick.RemoveAllListeners();
        _revertButton.onClick.AddListener(RevertSettings);

        _confirmButton?.onClick.RemoveAllListeners();
        _confirmButton?.onClick.AddListener(ConfirmSettings);
    }

    public void OpenSettings(SettingsCategory category)
    {
        _currentCategory = category;

        foreach (var section in _settingsSections)
        {
            bool isActive = section.Category == category;

            section.Panel?.SetActive(isActive);
            section.Label.text = isActive ? $"<u>{section.LabelName}</u>" : section.LabelName;
        }
    }

    public void OpenConfirmPanel()
    {
        _confirmPanel?.SetActive(true);
    }

    public void ConfirmSettings()
    {
        ApplySettings();
        _confirmPanel?.SetActive(false);
    }

    public void ApplySettings()
    {
        SettingsManager.Instance.SaveSettings();
    }

    public void RevertSettings()
    {
        SettingsManager.Instance.LoadSettings();
        _confirmPanel?.SetActive(false);
    }
}
