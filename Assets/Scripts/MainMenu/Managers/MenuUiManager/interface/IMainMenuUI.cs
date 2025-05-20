public interface IMainMenuUI
{
    void Init(ISettings settings, IGameFlow gameFlow);
    void OpenPanel(bool screen, bool sound, bool control);
    void OpenSettingsMenu();
    void CloseSettingsMenu();
    void OpenScreenSettings();
    void OpenSoundSettings();
    void OpenControlSettings();

}
