using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public interface ISettings
{
    void ChangeSFXVolume(float value);
    void ChangeMusicVolume(float value);

    float GetSFXVolume();
    float GetMusicVolume();

    void ChangeQuality(int index);
    int GetQualityLevel();

    void ChangeResolution(int index);
    int GetResolution();

    void ChangeWindowMode();
    bool GetIsWindowMode();

    void ChangeSensitivity(float value);
    float GetSensitivity();

    InputAction GetAction(string mapName, string actionName);
    InputActionAsset GetInputActionsAsset();

}
