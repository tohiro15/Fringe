public interface IMainMenuUI
{
    void Init(ISettings settings, IGameFlow gameFlow);
    void OpenChapterSelectionMenu();
    void CloseChapterSelectionMenu();
    void SelectChapter(ChapterData chapter);
    void InitializeChapterTitles();
    void OpenSettingsMenu();
    void CloseSettingsMenu();
    void SetCanvas(MenuState state);
}
