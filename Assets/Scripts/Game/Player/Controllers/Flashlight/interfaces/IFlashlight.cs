using UnityEngine.InputSystem;

public interface IFlashlight
{
    void Init(InputActionAsset inputAction, InputAction flashlightAction);
    void HandleInput();
    void ToggleFlashlight();
}
