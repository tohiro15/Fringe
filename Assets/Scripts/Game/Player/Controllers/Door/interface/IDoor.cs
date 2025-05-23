using UnityEngine;
using UnityEngine.InputSystem;

public interface IDoor
{
    void Init(InputActionAsset inputAction, InputAction interactAction, Camera playerCamera);
    void FindDoor();
}
