using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    [SerializeField] private GameFlowController _gameFlowController;
    [SerializeField] private MainMenuUIManager _mainMenuUIManager;
    [SerializeField] private MenuSoundManager _mainMenuSoundManager;
    [SerializeField] private SettingsManager _settingsManager;

    private IGameFlow _gameFlow;
    private IMainMenuUI _mainMenuUI;
    private IMainMenuSound _mainMenuSound;
    private ISettings _settings;

    private void Start()
    {
        _gameFlow = _gameFlowController;

        _mainMenuUI = _mainMenuUIManager;
        _settings = _settingsManager;
        _mainMenuSound = _mainMenuSoundManager;

        _mainMenuUI?.Init(_settings, _gameFlow);
    }

    public IGameFlow GetGameFlow()
    {
        return _gameFlow;
    }
}
