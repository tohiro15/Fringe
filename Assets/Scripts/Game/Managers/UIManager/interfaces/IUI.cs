public interface IUI
{
    void Init();
    void UpdateInteractionText(bool isActive, string text);
    void OpenPauseMenu();
    void OpenSettingsMenu();
    void ClosePauseMenu();
    void SetCanvas(bool game, bool pause, bool settings);
}
