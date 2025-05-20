public interface ISettings
{
    void ChangeQuality(int index);
    int GetQualityLevel();
    void ChangeResolution(int index);
    int GetResolution();
    void GetCurrentResolution();
    void ChangeWindowMode();
    bool GetIsWindowMode();
    void ChangeVolume(float value);
    float GetVolume();
    void ChangeSensitivity(float newSensitivity);
    float GetSensitivity();
}
