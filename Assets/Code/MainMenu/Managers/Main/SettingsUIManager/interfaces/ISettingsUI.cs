public interface ISettingsUI
{
    void Init(ISettings settings);
    void OpenSettings(SettingsCategory category);
    void OpenConfirmPanel();
    void ConfirmSettings();
    void ApplySettings();
    void RevertSettings();
}
