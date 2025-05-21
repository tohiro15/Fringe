using TMPro;
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
    }

    [SerializeField] private GameObject _startTriggerMenu;

    [SerializeField] private TMP_Text _interactText;

    private void Start()
    {
        OpenStartMenu();

        if (_interactText != null)
        {
            UpdateInteractionText(false);
        }
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

    public void UpdateInteractionText(bool isActive, string text = "Нажмите E чтобы открыть")
    {
        if(_interactText == null) return;
        _interactText.text = text;
        _interactText.gameObject.SetActive(isActive);
    }
}
