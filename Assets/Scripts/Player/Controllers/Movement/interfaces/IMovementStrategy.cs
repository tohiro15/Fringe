using UnityEngine;
public interface IMovementStrategy
{
    void Move(Rigidbody rb, Transform transform);
}
