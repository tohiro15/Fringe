using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    [SerializeField] private InputActionAsset _inputAction;
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private SettingsUIManager _settingsUIManager;
    public ISound Sound => _sound;

    private ISound _sound;
    private IUI _ui;
    private ISettingsUI _settingsUI;
    public GameState CurrentState { get; private set; }
    public bool IsCursorLocked { get; private set; }

    public event Action<GameState> OnGameStateChanged;

    private void Start()
    {
        if (_soundManager == null)
        {
            Debug.LogError("SoundManager not assigned!");
            return;
        }

        _sound = _soundManager;
        _ui = _uiManager;
        _settingsUI = _settingsUIManager;

        _ui?.Init();
        _settingsUI?.Init(SettingsManager.Instance);

        Debug.Log("Game Started");

        SetGameState(GameState.Playing);
        LockCursor();
    }

    public void SetGameState(GameState newState)
    {
        if (CurrentState == newState) return;

        CurrentState = newState;
        OnGameStateChanged?.Invoke(newState);

        if (newState == GameState.Playing)
            LockCursor();
        else
            UnlockCursor();
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        IsCursorLocked = true;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        IsCursorLocked = false;
    }

    public void ReturnToMainMenu() // временный способ перейти в главное меню
    {
        SceneManager.LoadScene(0);
    }

    public InputActionAsset GetInputActionAsset()
    {
        return _inputAction;
    }
}
