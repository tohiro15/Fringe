using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour, IMainMenuUI
{
    [Header("MainMenu")]
    [Space]

    [Header("Buttons")]
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _quitGameButton;

    [Header ("Settings")]
    [Space]

    [Header("Controllers")]

    [SerializeField] private ScreenSettingsUI _screenSettingsUI;
    [SerializeField] private SoundSettingsUI _soundSettingsUI;
    [SerializeField] private ControlSettingsUI _controlSettingsUI;

    [Header("Canvases")]

    [SerializeField] private Canvas _settingsCanvas;

    [Header("Buttons")]

    [SerializeField] private Button _openScreenSettingsButton;
    [SerializeField] private Button _openSoundSettingsButton;
    [SerializeField] private Button _openControlSettingsButton;

    [Space]

    [SerializeField] private Button _closeSettingsButton;

    [Header("Text")]
    [SerializeField] private TMP_Text _screenButtonText;
    [SerializeField] private TMP_Text _soundButtonText;
    [SerializeField] private TMP_Text _controlButtonText;
    public void Init(ISettings settings, IGameFlow gameFlow)
    {
        if(_settingsCanvas != null) _settingsCanvas.gameObject.SetActive(false);

        _screenSettingsUI?.Init(settings);
        _soundSettingsUI?.Init(settings);
        _controlSettingsUI?.Init(settings);

        _startGameButton?.onClick.AddListener(gameFlow.StartGame);
        _settingsButton?.onClick.AddListener(OpenSettingsMenu);
        _quitGameButton?.onClick.AddListener(gameFlow.QuitGame);

        _openScreenSettingsButton?.onClick.AddListener(OpenScreenSettings);
        _openSoundSettingsButton?.onClick?.AddListener(OpenSoundSettings);
        _openControlSettingsButton?.onClick.AddListener(OpenControlSettings);

        _closeSettingsButton?.onClick.AddListener(CloseSettingsMenu);

        OpenPanel(false, false, false);
    }

    public void OpenPanel(bool screen, bool sound, bool control)
    {
        _screenSettingsUI.gameObject.SetActive(screen);
        _soundSettingsUI.gameObject.SetActive(sound);
        _controlSettingsUI.gameObject.SetActive(control);
    }

    public void OpenSettingsMenu()
    {
        _settingsCanvas?.gameObject.SetActive(true);
    }

    public void CloseSettingsMenu()
    {
        _settingsCanvas?.gameObject.SetActive(false);
    }

    public void OpenScreenSettings()
    {

        _screenButtonText.text = "<u>�����</u>";
        _soundButtonText.text = "����";
        _controlButtonText.text = "����������";

        OpenPanel(true, false, false);
    }

    public void OpenSoundSettings()
    {
        _screenButtonText.text = "�����";
        _soundButtonText.text = "<u>����</u>";
        _controlButtonText.text = "����������";

        OpenPanel(false, true, false);
    }

    public void OpenControlSettings()
    {
        _screenButtonText.text = "�����";
        _soundButtonText.text = "����";
        _controlButtonText.text = "<u>����������</u>";

        OpenPanel(false, false, true);
    }
}
