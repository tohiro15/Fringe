using UnityEngine;
using UnityEngine.InputSystem;

public interface IJump
{
    void Init(InputAction jumpAction);
    void CheckGround();
    void HandleInput(Rigidbody rb, float jumpForce);
}
