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

    [Header("Game HUD")]
    [Space]

    [SerializeField] private TMP_Text _interactText;

    [Header("Pause Menu")]
    [Space]

    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _mainMenuReturnButton;
    private void Start()
    {
        _gameCanvas?.gameObject.SetActive(true);
        _pauseCanvas?.gameObject.SetActive(false);

        if (_interactText != null)
        {
            UpdateInteractionText(false);
        }

        _continueButton?.onClick.RemoveAllListeners();
        _continueButton?.onClick.AddListener(ClosePauseMenu);

        _mainMenuReturnButton?.onClick.RemoveAllListeners();
        _mainMenuReturnButton?.onClick.AddListener(GameManager.Instance.ReturnToMainMenu);
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
        _gameCanvas?.gameObject.SetActive(false);
        _pauseCanvas?.gameObject.SetActive(true);
    }

    public void ClosePauseMenu()
    {
        GameManager.Instance.SetGameState(GameState.Playing);
        _gameCanvas?.gameObject.SetActive(true);
        _pauseCanvas?.gameObject.SetActive(false);
    }
}
