using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField] private MenuUIManager _mainMenuUIManager;
    [SerializeField] private MenuSoundManager _mainMenuSoundManager;

    private IMainMenuUI _mainMenuUI;
    private IMainMenuSound _mainMenuSound;

    private void Start()
    {
        _mainMenuUI = _mainMenuUIManager;
        _mainMenuSound = _mainMenuSoundManager;
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
