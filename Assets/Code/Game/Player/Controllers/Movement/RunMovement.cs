using UnityEngine;
using UnityEngine.InputSystem;

public class RunMovement : IMovementStrategy
{
    private float _speed;
    private IAnimation _animation;

    private Rigidbody _rigidbody;

    private InputAction _moveAction;

    private Vector2 _moveAmt;

    public RunMovement(InputAction moveAction, IAnimation animation, Rigidbody rigidbody, float speed)
    {
        _moveAction = moveAction;

        _animation = animation;
        _rigidbody = rigidbody;
        _speed = speed;
    }

    public float GetSpeed() => _speed;

    public void Move(Transform transform)
    {
        _moveAmt = _moveAction.ReadValue<Vector2>();

        Vector3 input = transform.forward * _moveAmt.y + transform.right * _moveAmt.x;
        Vector3 direction = input.normalized * _speed * Time.deltaTime;
        _rigidbody.MovePosition(_rigidbody.position + direction);

        HandleAnimation(input);
    }

    public void HandleAnimation(Vector3 inputDirection)
    {
        if (inputDirection == Vector3.zero)
            _animation?.PlayIdle();
        else
            _animation?.PlayRun();
    }
}
