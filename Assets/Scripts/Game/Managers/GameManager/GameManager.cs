using System;
using UnityEngine;
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

    [SerializeField] private SoundManager _soundManager;
    public ISound Sound => _sound;

    private ISound _sound;
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

        Debug.Log("Game Started");

        SetGameState(GameState.Playing);
        LockCursor();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene(0);
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
}
