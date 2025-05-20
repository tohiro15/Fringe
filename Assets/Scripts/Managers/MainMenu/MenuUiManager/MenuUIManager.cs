using UnityEngine;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour, IMainMenuUI
{
    [Header("Canvases")]
    [Space]

    [SerializeField] private Canvas _mainMenuCanvas;
    [SerializeField] private Canvas _settingsCanvas;

    [Header("Buttons")]
    [Space]

    [Header("MainMenu Buttons")]

    [SerializeField] private Button _startButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _exitButton;

    [Header("Settings Buttons")]
    [SerializeField] private Button _exitSettingsButton;

    private void Awake()
    {
        _mainMenuCanvas?.gameObject?.SetActive(true);
        _settingsCanvas?.gameObject?.SetActive(false);
    }
    private void Start()
    {
        _startButton?.onClick.AddListener(MainMenuManager.Instance.StartGame);

        _settingsButton?.onClick.AddListener(OpenSettingsMenu);

        _exitButton?.onClick.AddListener(MainMenuManager.Instance.QuitGame);;

        _exitSettingsButton?.onClick.AddListener(CloseSettingsMenu);
    }
    public void OpenSettingsMenu()
    {
        _settingsCanvas?.gameObject.SetActive(true);
    }

    public void CloseSettingsMenu()
    {
        _settingsCanvas?.gameObject.SetActive(false);
    }
}
