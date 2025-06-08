public interface IGameFlow
{
    void SetChapter(ChapterData chapter);
    ChapterData GetChapter();
    void StartGame();
    void QuitGame();
}
