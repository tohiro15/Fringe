using UnityEngine;

public interface IRotatable
{
    Camera GetCamera();
    void HandleInput();
    void Rotate(Rigidbody rb);
}
