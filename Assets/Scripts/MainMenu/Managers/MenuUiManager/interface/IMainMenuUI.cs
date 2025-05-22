public interface IMainMenuUI
{
    void Init(ISettings settings, IGameFlow gameFlow);
    void OpenSettingsMenu();
    void CloseSettingsMenu();
    void OpenSettings(SettingsCategory category);
    void SetCanvas(bool main, bool settings);
    void SetTextUnderlining(bool screenUnderline, bool qualityUnderline, bool soundUnderline, bool controlUnderline);

}
