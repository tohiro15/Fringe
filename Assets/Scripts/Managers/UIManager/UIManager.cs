using UnityEngine;

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
        DontDestroyOnLoad(gameObject);
    }

    [SerializeField] private GameObject _startTriggerMenu;

    private void Start()
    {
        OpenStartMenu();
    }

    public void OpenStartMenu()
    {
        if (_startTriggerMenu == null) return;

        _startTriggerMenu.SetActive(true);
        GameManager.Instance.SetGameState(GameState.StartMenu);
    }
    public void CloseStartMenu()
    {
        if (_startTriggerMenu == null) return;

        _startTriggerMenu.SetActive(false);
        GameManager.Instance.SetGameState(GameState.Playing);
    }
}
