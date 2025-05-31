using UnityEngine;

public interface IUI
{
    void Init();
    void UpdateInteractionImage(bool isActive);
    void OpenPauseMenu();
    void OpenSettingsMenu();
    void ClosePauseMenu();
    void SetCanvas(bool game, bool pause, bool settings);
}
