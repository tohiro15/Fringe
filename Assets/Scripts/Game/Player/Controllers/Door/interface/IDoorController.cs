using UnityEngine;
using UnityEngine.InputSystem;

public interface IDoorController
{
    void Init(InputAction interactAction, Camera playerCamera);
    void FindDoorAndInteract(float interactDistance);
}
