using UnityEngine;

public interface IMovementStrategy
{
    float GetSpeed();
    void Move(Rigidbody rb, Transform transform);
    void HandleAnimation(Vector3 inputDirection);
}

