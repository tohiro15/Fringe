public interface IMainMenuUI
{
    void Init(ISettings settings, IGameFlow gameFlow);
    void OpenChapterSelectionMenu();
    void CloseChapterSelectionMenu();
    void SelectChapter(int index);
    void InitializeChapterTitles();
    void OpenSettingsMenu();
    void CloseSettingsMenu();
    void OpenSettings(SettingsCategory category);
    void SetCanvas(MenuState state);


}
