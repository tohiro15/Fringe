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

    [Header("Settings")]
    [Space]

    [Header("Buttons")]
    [SerializeField] private Button _exitButton;

    [Header("Chapter Selection")]
    [Space]

    [Header("Buttons")]
    [SerializeField] private ChapterButton[] _chapterButtons;
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _backButton;

    private ChapterData _selectedChapter;

    public void Init(ISettings settings, IGameFlow gameFlow)
    {
        if(_settingsCanvas != null) _settingsCanvas.gameObject.SetActive(false);

        _chapterSelectionStartButton?.onClick.RemoveAllListeners();
        _chapterSelectionStartButton?.onClick.AddListener(OpenChapterSelectionMenu);

        _settingsButton?.onClick.RemoveAllListeners();
        _settingsButton?.onClick.AddListener(OpenSettingsMenu);

        _quitGameButton?.onClick.RemoveAllListeners();
        _quitGameButton?.onClick.AddListener(gameFlow.QuitGame);

        _exitButton?.onClick.RemoveAllListeners();
        _exitButton?.onClick.AddListener(CloseSettingsMenu);

        for (int i = 0; i < _chapterButtons.Length; i++)
        {
            _chapterButtons[i].Init(SelectChapter);
        }

        if (_startGameButton != null) _startGameButton.interactable = false;

        _startGameButton?.onClick.RemoveAllListeners();
        _startGameButton?.onClick.AddListener(() =>
        {
            if (_selectedChapter != null)
            {
                MainMenuManager.Instance.GetGameFlow().StartGame();
            }
        });

        _backButton?.onClick.RemoveAllListeners();
        _backButton?.onClick.AddListener(CloseChapterSelectionMenu);

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
    public void SelectChapter(ChapterData chapter)
    {
        _selectedChapter = chapter;

        MainMenuManager.Instance.GetGameFlow().SetChapter(chapter);

        if (_startGameButton != null)
            _startGameButton.interactable = true;

        foreach (var chBtn in _chapterButtons)
        {
            chBtn.Highlight(chBtn.ChapterData == chapter);
        }
    }


    public void InitializeChapterTitles()
    {
        for (int i = 0; i < _chapterButtons.Length; i++)
        {
            TMP_Text textComponent = _chapterButtons[i].GetComponentInChildren<TMP_Text>();

            if (textComponent != null)
            {
                string label = (i < _chapterButtons.Length)
                    ? $"ÃËÀÂÀ \"{_chapterButtons[i].ChapterData.chapterName}\""
                    : $"ÃËÀÂÀ - {i + 1}";

                textComponent.text = label;
            }
        }
    }


    public void OpenSettingsMenu()
    {
        _settingsCanvas.gameObject.SetActive(true);
        SettingsUIManager.Instance.OpenSettings(SettingsCategory.Screen);
    }

    public void CloseSettingsMenu()
    {
        SetCanvas(MenuState.Main);
        _settingsCanvas?.gameObject.SetActive(false);
    }
    public void SetCanvas(MenuState state)
    {
        _mainMenuCanvas?.gameObject.SetActive(state == MenuState.Main);
        _chapterSelectionCanvas?.gameObject.SetActive(state == MenuState.Chapter);
        _settingsCanvas?.gameObject.SetActive(state == MenuState.Settings);
    }

}
