using UnityEngine;

public interface ICrouch
{
    bool GetIsCrouching();
    bool GetWantsToStant();
    void Init(Camera playerCamera, IAnimation animation);
    void HandleInput();
    void Crouch();
    void StandUp();
}
