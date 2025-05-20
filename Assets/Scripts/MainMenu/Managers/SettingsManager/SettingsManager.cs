using UnityEngine;
using UnityEngine.Audio;

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

    [SerializeField] private AudioMixer _mainAudioMixer;

    private int _currentResolutionIndex;
    private int _currentQualityLevel;

    private float _mouseSensitivity = 80f;

    private bool _windowMode = false;

    private void Start()
    {
        _currentQualityLevel = QualitySettings.GetQualityLevel();
        GetCurrentResolution();
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
    public void ChangeVolume(float value)
    {
        if (_mainAudioMixer == null) return;

        float volumePercent = Mathf.Clamp(value, 0.0001f, 100f);
        float dB = Mathf.Log10(volumePercent / 100f) * 40f;
        _mainAudioMixer.SetFloat("MainVolume", dB);
    }

    public float GetVolume()
    {
        _mainAudioMixer.GetFloat("MainVolume", out float dBValue);
        float volumePercent = Mathf.Pow(10f, dBValue / 40f) * 100f;

        return volumePercent;
    }
    #endregion
    #region MouseSensitivitySettings
    public void ChangeSensitivity(float newSensitivity)
    {
        _mouseSensitivity = newSensitivity; ;
    }

    public float GetSensitivity()
    {
        return _mouseSensitivity;
    }
    #endregion
}
