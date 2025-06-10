using UnityEngine;
using UnityEngine.InputSystem;

public interface ICrouch
{
    void Init(InputAction crouchAction, Camera playerCamera, IAnimation animation);
    bool GetIsCrouching();
    bool GetWantsToStant();
    void HandleInput();
    void Crouch();
    void StandUp();
    bool CanStandUp();
}
