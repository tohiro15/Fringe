using UnityEngine;
using UnityEngine.InputSystem;

public interface IRotatable
{
    void Init(InputAction lookAction);
    Camera GetCamera();
    void HandleInput();
    void Rotate(Rigidbody rb);
}
