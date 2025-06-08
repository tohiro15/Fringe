using UnityEngine;

public interface IMovementStrategy
{
    float GetSpeed();
    void Move(Transform transform);
    void HandleAnimation(Vector3 inputDirection);
}

