using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour, ISettings
{
    public static SettingsManager Instance { get; private set; }
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

    [Header("Control Settings")]
    [Space]

    [SerializeField] private InputActionAsset _inputActions;

    [Header("Sound Settings")]
    [Space]

    [SerializeField] private AudioMixer _SFXAudioMixer;
    [SerializeField] private AudioMixer _musicAudioMixer;

    private int _currentResolutionIndex;
    private int _currentQualityLevel;

    private float _mouseSensitivity = 80f;

    private bool _windowMode = false;

    private void Start()
    {
        _currentQualityLevel = QualitySettings.GetQualityLevel();
        GetCurrentResolution();

        if (_SFXAudioMixer == null) Debug.LogError("Микшер SFX - не инициализирован!");
        if (_musicAudioMixer == null) Debug.LogError("Микшер Music - не инициализирован!");
    }

    #region QualitySettings
    public void ChangeQuality(int index)
    {
        _currentQualityLevel = index;
        QualitySettings.SetQualityLevel(index);
    }

    public int GetQualityLevel()
    {
        return _currentQualityLevel;
    }
    #endregion

    #region ResolutionSettings
    public void ChangeResolution(int index)
    {
        Resolution[] resolutions = Screen.resolutions;
        if (index < resolutions.Length)
        {
            Resolution res = resolutions[index];
            Screen.SetResolution(res.width, res.height, Screen.fullScreen);
        }
    }

    public int GetResolution()
    {
        return _currentResolutionIndex;
    }
    public void GetCurrentResolution()
    {
        Resolution currentRes = Screen.currentResolution;
        Resolution[] resolutions = Screen.resolutions;

        _currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == currentRes.width &&
                resolutions[i].height == currentRes.height &&
                Mathf.RoundToInt((float)resolutions[i].refreshRateRatio.value) == Mathf.RoundToInt((float)currentRes.refreshRateRatio.value))
            {
                _currentResolutionIndex = i;
                break;
            }
        }
    }
    #endregion

    #region WindowModeSettings
    public void ChangeWindowMode()
    {
        _windowMode = !_windowMode;
        Screen.fullScreen = !_windowMode;
    }
    public bool GetIsWindowMode()
    {
        return _windowMode;
    }
    #endregion

    #region VolumeSettings
    public void ChangeSFXVolume(float value)
    {
        if (_SFXAudioMixer == null) return;

        float volumePercent = Mathf.Clamp(value, 0.0001f, 100f);
        float dB = Mathf.Log10(volumePercent / 100f) * 40f;
        _SFXAudioMixer.SetFloat("MainVolume", dB);
    }

    public void ChangeMusicVolume(float value)
    {
        if (_musicAudioMixer == null) return;

        float volumePercent = Mathf.Clamp(value, 0.0001f, 100f);
        float dB = Mathf.Log10(volumePercent / 100f) * 40f;
        _musicAudioMixer.SetFloat("MainVolume", dB);
    }

    public float GetSFXVolume()
    {
        _SFXAudioMixer.GetFloat("MainVolume", out float dBValue);
        float volumePercent = Mathf.Pow(10f, dBValue / 40f) * 100f;

        return volumePercent;
    }

    public float GetMusicVolume()
    {
        _musicAudioMixer.GetFloat("MainVolume", out float dBValue);
        float volumePercent = Mathf.Pow(10f, dBValue / 40f) * 100f;

        return volumePercent;
    }

    #endregion

    #region MouseSensitivitySettings
    public void ChangeSensitivity(float newSensitivity)
    {
        _mouseSensitivity = newSensitivity;
    }

    public float GetSensitivity()
    {
        return _mouseSensitivity;
    }
    #endregion

    #region ControlSettings

    public InputAction GetAction(string mapName, string actionName)
    {
        return _inputActions.FindActionMap(mapName)?.FindAction(actionName);
    }

    public InputActionAsset GetInputActionsAsset()
    {
        return _inputActions;
    }

    #endregion
}
