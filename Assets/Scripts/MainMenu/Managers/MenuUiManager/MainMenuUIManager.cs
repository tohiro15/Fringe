using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour, IMainMenuUI
{
    [Header("MainMenu")]
    [Space]

    [Header("Buttons")]
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _quitGameButton;

    [Header ("Settings")]
    [Space]

    [Header("Controllers")]

    [SerializeField] private ScreenSettingsUI _screenSettingsUI;
    [SerializeField] private QualitySettingsUI _qualitySettingsUI;
    [SerializeField] private SoundSettingsUI[] _soundSettingsUI;
    [SerializeField] private ControlSettingsUI _controlSettingsUI;

    [Header("Canvases")]

    [SerializeField] private Canvas _mainMenuCanvas;
    [SerializeField] private Canvas _settingsCanvas;

    [Header("Settings Sections")]

    [SerializeField] private List<SettingsSection> _settingsSections;

    [Space]

    [SerializeField] private Button _closeSettingsButton;

    [Header("Text")]
    [SerializeField] private TMP_Text _screenButtonText;
    [SerializeField] private TMP_Text _qualityButtonText;
    [SerializeField] private TMP_Text _soundButtonText;
    [SerializeField] private TMP_Text _controlButtonText;
    private SettingsCategory _currentCategory;

    public void Init(ISettings settings, IGameFlow gameFlow)
    {
        if(_settingsCanvas != null) _settingsCanvas.gameObject.SetActive(false);

        _screenSettingsUI?.Init(settings);
        _qualitySettingsUI?.Init(settings);

        foreach (var soundSettings in _soundSettingsUI)
        {
            soundSettings.Init(settings);
        }

        _controlSettingsUI?.Init(settings);

        _startGameButton?.onClick.RemoveAllListeners();
        _startGameButton?.onClick.AddListener(gameFlow.StartGame);

        _settingsButton?.onClick.RemoveAllListeners();
        _settingsButton?.onClick.AddListener(OpenSettingsMenu);

        _quitGameButton?.onClick.RemoveAllListeners();
        _quitGameButton?.onClick.AddListener(gameFlow.QuitGame);

        _closeSettingsButton?.onClick.RemoveAllListeners();
        _closeSettingsButton?.onClick.AddListener(CloseSettingsMenu);

        foreach (var section in _settingsSections)
        {
            SettingsCategory capturedCategory = section.Category;
            section.Button?.onClick.AddListener(() => OpenSettings(capturedCategory));
        }
    }

    public void OpenSettingsMenu()
    {
        SetCanvas(false, true);
        OpenSettings(SettingsCategory.Screen);
    }

    public void CloseSettingsMenu()
    {
        SetCanvas(true, false);
        _settingsCanvas?.gameObject.SetActive(false);
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
    public void SetCanvas(bool main, bool settings)
    {
        _mainMenuCanvas?.gameObject.SetActive(main);
        _settingsCanvas?.gameObject.SetActive(settings);
    }
    public void SetTextUnderlining(bool screenUnderline, bool qualityUnderline, bool soundUnderline, bool controlUnderline)
    {
        if(screenUnderline) _screenButtonText.text = "<u>Экран</u>";
        else _screenButtonText.text = "Экран";

        if(qualityUnderline) _qualityButtonText.text = "<u>Графика</u>";
        else _qualityButtonText.text = "Графика";

        if (soundUnderline) _soundButtonText.text = "<u>Звук</u>";
        else _soundButtonText.text = "Звук";

        if(controlUnderline) _controlButtonText.text = "<u>Управление</u>";
        else _controlButtonText.text = "Управление";
    }
}
