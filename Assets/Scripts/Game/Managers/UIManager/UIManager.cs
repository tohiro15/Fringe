using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IUI
{
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    [Header("Canvases")]

    [SerializeField] private Canvas _gameCanvas;
    [SerializeField] private Canvas _pauseCanvas;
    [SerializeField] private Canvas _settingsCanvas;

    [Header("Game HUD")]
    [Space]

    [SerializeField] private TMP_Text _interactText;

    [Header("Pause Menu")]
    [Space]

    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _mainMenuReturnButton;

    [Header("Settings Menu")]
    [Space]

    [SerializeField] private Button _exitButton;
    public void Init()
    {
        _gameCanvas?.gameObject.SetActive(true);
        _pauseCanvas?.gameObject.SetActive(false);

        if (_interactText != null)
        {
            UpdateInteractionText(false);
        }

        _continueButton?.onClick.RemoveAllListeners();
        _continueButton?.onClick.AddListener(ClosePauseMenu);

        _settingsButton?.onClick.RemoveAllListeners();
        _settingsButton?.onClick.AddListener(OpenSettingsMenu);

        _mainMenuReturnButton?.onClick.RemoveAllListeners();
        _mainMenuReturnButton?.onClick.AddListener(GameManager.Instance.ReturnToMainMenu);

        _exitButton.onClick.RemoveAllListeners();
        _exitButton.onClick.AddListener(OpenPauseMenu);

        SetCanvas(true, false,false);
    }

    public void UpdateInteractionText(bool isActive, string text = "Нажмите 'E' чтобы открыть")
    {
        if(_interactText == null) return;
        _interactText.text = text;
        _interactText.gameObject.SetActive(isActive);
    }

    public void OpenPauseMenu()
    {
        GameManager.Instance.SetGameState(GameState.Pause);
        SetCanvas(false, true, false);
    }

    public void OpenSettingsMenu()
    {
        SetCanvas(false, false, true);
        SettingsUIManager.Instance.OpenSettings(SettingsCategory.Screen);
    }



    public void ClosePauseMenu()
    {
        GameManager.Instance.SetGameState(GameState.Playing);
        SetCanvas(true, false, false);
    }

    public void SetCanvas(bool game, bool pause, bool settings)
    {
        _gameCanvas?.gameObject.SetActive(game);
        _pauseCanvas?.gameObject.SetActive(pause);
        _settingsCanvas?.gameObject.SetActive(settings);
    }
}
