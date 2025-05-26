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

    [SerializeField] private TMP_Text _interactText;

    private void Start()
    {
        if (_interactText != null)
        {
            UpdateInteractionText(false);
        }
    }

    public void UpdateInteractionText(bool isActive, string text = "Нажмите 'E' чтобы открыть")
    {
        if(_interactText == null) return;
        _interactText.text = text;
        _interactText.gameObject.SetActive(isActive);
    }
}
