using UnityEngine;
using UnityEngine.InputSystem;

public enum MovementState { Walk, Run }

public class MovementStrategy : IMovementStrategy
{
    public float Speed { get; }
    private readonly IAnimation _animation;
    private readonly Rigidbody _rb;
    private readonly InputAction _moveAction;
    private readonly MovementState _state;

    public MovementStrategy(InputAction moveAction, IAnimation animation, Rigidbody rb, float speed, MovementState state)
    {
        _moveAction = moveAction;
        _animation = animation;
        _rb = rb;
        Speed = speed;
        _state = state;
    }

    public void Move(Transform transform)
    {
        Vector2 raw = _moveAction.ReadValue<Vector2>();
        Vector3 dir = transform.forward * raw.y + transform.right * raw.x;
        Vector3 movement = dir.normalized * Speed * Time.deltaTime;
        _rb.MovePosition(_rb.position + movement);
        HandleAnimation(dir);
    }

    public void HandleAnimation(Vector3 inputDirection)
    {
        if (inputDirection == Vector3.zero)
        {
            _animation.PlayIdle();
            return;
        }
        if (_state == MovementState.Walk)
            _animation.PlayWalk();
        else
            _animation.PlayRun();
    }
}
