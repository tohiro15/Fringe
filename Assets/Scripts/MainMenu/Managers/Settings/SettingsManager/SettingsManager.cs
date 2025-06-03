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

    private float _mouseSensitivity = 10f;

    public InputActionAsset InputActions => _inputActions;
    public float Sensitivity => _mouseSensitivity;

    private bool _windowMode = false;

    private void Start()
    {
        _currentQualityLevel = QualitySettings.GetQualityLevel();
        GetCurrentResolution();

        if (_SFXAudioMixer == null) Debug.LogError("������ SFX - �� ���������������!");
        if (_musicAudioMixer == null) Debug.LogError("������ Music - �� ���������������!");

        LoadSettings();
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

    public void LoadSettings()
    {
        _mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 10f);
        _currentQualityLevel = PlayerPrefs.GetInt("QualityLevel", QualitySettings.GetQualityLevel());
        _windowMode = PlayerPrefs.GetInt("WindowMode", 0) == 1;
        ChangeQuality(_currentQualityLevel);
        Screen.fullScreen = !_windowMode;
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("MouseSensitivity", _mouseSensitivity);
        PlayerPrefs.SetInt("QualityLevel", _currentQualityLevel);
        PlayerPrefs.SetInt("WindowMode", _windowMode ? 1 : 0);
        PlayerPrefs.Save();
    }

}
