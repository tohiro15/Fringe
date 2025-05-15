using UnityEngine;

public interface IRotatable
{
    void HandleInput();
    void Rotate(Rigidbody rb);
}
