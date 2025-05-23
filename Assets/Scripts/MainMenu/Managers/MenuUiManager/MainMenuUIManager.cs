using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum MenuState 
{
    Main, Chapter, Settings 
}

public class MainMenuUIManager : MonoBehaviour, IMainMenuUI
{
    [Header("Canvases")]

    [SerializeField] private Canvas _mainMenuCanvas;
    [SerializeField] private Canvas _settingsCanvas;
    [SerializeField] private Canvas _chapterSelectionCanvas;

    [Header("MainMenu")]
    [Space]

    [Header("Buttons")]
    [SerializeField] private Button _chapterSelectionStartButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _quitGameButton;

    [Header("Chapter Selection")]
    [Space]

    [Header("Buttons")]
    [SerializeField] private string[] _chapterTitles;
    [SerializeField] private Button[] _chapterSelectButtons;
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _backButton;

    [Header ("Settings")]
    [Space]

    [Header("Controllers")]

    [SerializeField] private ScreenSettingsUI _screenSettingsUI;
    [SerializeField] private QualitySettingsUI _qualitySettingsUI;
    [SerializeField] private SoundSettingsUI[] _soundSettingsUI;
    [SerializeField] private ControlSettingsUI _controlSettingsUI;


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
    private int _selectedChapterIndex = -1;

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

        _chapterSelectionStartButton?.onClick.RemoveAllListeners();
        _chapterSelectionStartButton?.onClick.AddListener(OpenChapterSelectionMenu);

        _settingsButton?.onClick.RemoveAllListeners();
        _settingsButton?.onClick.AddListener(OpenSettingsMenu);

        _quitGameButton?.onClick.RemoveAllListeners();
        _quitGameButton?.onClick.AddListener(gameFlow.QuitGame);

        _closeSettingsButton?.onClick.RemoveAllListeners();
        _closeSettingsButton?.onClick.AddListener(CloseSettingsMenu);

        for (int i = 0; i < _chapterSelectButtons.Length; i++)
        {
            int index = i;
            _chapterSelectButtons[i]?.onClick.RemoveAllListeners();
            _chapterSelectButtons[i]?.onClick.AddListener(() =>
            {
                SelectChapter(index);
            });
        }

        if (_startGameButton != null) _startGameButton.interactable = false;

        _startGameButton?.onClick.RemoveAllListeners();
        _startGameButton?.onClick.AddListener(() =>
        {
            if (MainMenuManager.Instance.GetGameFlow().GetChapterIndex() >= 0)
            {
                MainMenuManager.Instance.GetGameFlow().StartGame();
            }
        });

        _backButton?.onClick.RemoveAllListeners();
        _backButton?.onClick.AddListener(CloseChapterSelectionMenu);

        foreach (var section in _settingsSections)
        {
            SettingsCategory capturedCategory = section.Category;
            section.Button?.onClick.AddListener(() => OpenSettings(capturedCategory));
        }

        SetCanvas(MenuState.Main);
        InitializeChapterTitles();
    }

    public void OpenChapterSelectionMenu()
    {
        SetCanvas(MenuState.Chapter);
    }

    public void CloseChapterSelectionMenu()
    {
        SetCanvas(MenuState.Main);
    }
    public void SelectChapter(int index)
    {
        _selectedChapterIndex = index;

        MainMenuManager.Instance.GetGameFlow().SetChapterIndex(index + 1);

        if (_startGameButton != null)
            _startGameButton.interactable = true;

        for (int i = 0; i < _chapterSelectButtons.Length; i++)
        {
            TMP_Text textComponent = _chapterSelectButtons[i].GetComponentInChildren<TMP_Text>();
            if (textComponent != null)
            {
                string label = _chapterTitles != null && i < _chapterTitles.Length
                    ? $"ÃËÀÂÀ \"{_chapterTitles[i]}\""
                    : $"ÃËÀÂÀ - {i + 1}";

                textComponent.text = (i == index) ? $"<u>{label}</u>" : label;
            }
        }
    }

    public void InitializeChapterTitles()
    {
        for (int i = 0; i < _chapterSelectButtons.Length; i++)
        {
            TMP_Text textComponent = _chapterSelectButtons[i].GetComponentInChildren<TMP_Text>();
            if (textComponent != null)
            {
                string label = _chapterTitles != null && i < _chapterTitles.Length
                    ? $"ÃËÀÂÀ \"{_chapterTitles[i]}\""
                    : $"ÃËÀÂÀ - {i + 1}";

                textComponent.text = label;
            }
        }
    }

    public void OpenSettingsMenu()
    {
        SetCanvas(MenuState.Settings);
        OpenSettings(SettingsCategory.Screen);
    }

    public void CloseSettingsMenu()
    {
        SetCanvas(MenuState.Main);
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
    public void SetCanvas(MenuState state)
    {
        _mainMenuCanvas?.gameObject.SetActive(state == MenuState.Main);
        _chapterSelectionCanvas?.gameObject.SetActive(state == MenuState.Chapter);
        _settingsCanvas?.gameObject.SetActive(state == MenuState.Settings);
    }

}
