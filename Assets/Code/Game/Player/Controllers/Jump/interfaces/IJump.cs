using UnityEngine;
using UnityEngine.InputSystem;

public interface IJump
{
    void Init(InputActionAsset inputAction, InputAction jumpAction);
    void CheckGround();
    void HandleInput(Rigidbody rb, float jumpForce);
}
