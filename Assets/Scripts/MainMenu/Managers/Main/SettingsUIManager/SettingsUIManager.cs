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

    public void Init(ISettings settings)
    {
        _screenSettingsUI?.Init(settings);
        _qualitySettingsUI?.Init(settings);
        _controlSettingsUI?.Init(settings);

        foreach (var soundSettings in _soundSettingsUI)
        {
            soundSettings.Init(settings);
        }

        foreach (var section in _settingsSections)
        {
            SettingsCategory capturedCategory = section.Category;
            section.Button?.onClick.AddListener(() => OpenSettings(capturedCategory));
        }
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
}
