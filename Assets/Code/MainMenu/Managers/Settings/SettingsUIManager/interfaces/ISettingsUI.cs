public interface ISettingsUI
{
    void Init();
    void OpenSettings(SettingsCategory category);
    void OpenConfirmPanel();
    void ConfirmSettings();
    void ApplySettings();
    void RevertSettings();
}
