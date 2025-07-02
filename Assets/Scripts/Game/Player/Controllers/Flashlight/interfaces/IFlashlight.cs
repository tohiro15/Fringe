using UnityEngine.InputSystem;

public interface IFlashlight
{
    void Init(InputAction flashlightAction);
    void HandleInput();
    void ToggleFlashlight();
}
