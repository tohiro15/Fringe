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
    private float _currentSFXVolume;
    private float _currentMusicVolume;

    private float _mouseSensitivity = 10f;

    private bool _windowMode = false;
    public bool IsChanged { get; private set; } = false;

    private void Start()
    {
        _currentQualityLevel = QualitySettings.GetQualityLevel();
        GetCurrentResolution();

        if (_SFXAudioMixer == null) Debug.LogError("Микшер SFX - не инициализирован!");
        if (_musicAudioMixer == null) Debug.LogError("Микшер Music - не инициализирован!");

        LoadSettings();
    }

    #region QualitySettings
    public void ChangeQuality(int index)
    {
        _currentQualityLevel = index;
        QualitySettings.SetQualityLevel(index);
        IsChanged = true;
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

        IsChanged = true;
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
        _currentSFXVolume = volumePercent;

        float dB = Mathf.Log10(volumePercent / 100f) * 40f;
        _SFXAudioMixer.SetFloat("MainVolume", dB);

        IsChanged = true;
    }

    public void ChangeMusicVolume(float value)
    {
        if (_musicAudioMixer == null) return;

        float volumePercent = Mathf.Clamp(value, 0.0001f, 100f);
        _currentMusicVolume = volumePercent;

        float dB = Mathf.Log10(volumePercent / 100f) * 40f;
        _musicAudioMixer.SetFloat("MainVolume", dB);

        IsChanged = true;
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

        IsChanged = true;
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

    public void SaveSettings()
    {
        if (!IsChanged) return;

        PlayerPrefs.SetInt("Resolution", _currentResolutionIndex);
        PlayerPrefs.SetInt("Quality", _currentQualityLevel);
        PlayerPrefs.SetFloat("SFXVolume", _currentSFXVolume);
        PlayerPrefs.SetFloat("MusicVolume", _currentMusicVolume);
        PlayerPrefs.SetFloat("Sensitivity", _mouseSensitivity);

        PlayerPrefs.Save();

        IsChanged = false;
    }

    public void LoadSettings()
    {
        _currentResolutionIndex = PlayerPrefs.GetInt("Resolution");
        ChangeResolution(_currentResolutionIndex);
        _currentQualityLevel = PlayerPrefs.GetInt("Quality");
        ChangeQuality(_currentQualityLevel);

        _currentSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 100f);
        ChangeSFXVolume(_currentSFXVolume);
        _currentMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 100f);
        ChangeMusicVolume(_currentMusicVolume);

        _mouseSensitivity = PlayerPrefs.GetFloat("Sensitivity", 100f);
        ChangeSensitivity(_mouseSensitivity);

        IsChanged = false;
    }

}
