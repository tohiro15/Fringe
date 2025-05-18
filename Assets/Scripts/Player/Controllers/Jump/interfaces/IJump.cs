using UnityEngine;

public interface IJump
{
    void CheckGround();
    void HandleInput(Rigidbody rb);
}
