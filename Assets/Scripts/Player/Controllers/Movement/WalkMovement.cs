using UnityEngine;

public class WalkMovement : IMovementStrategy
{
    private float _speed;
    private IAnimation _animation;

    public WalkMovement(IAnimation animation, float speed)
    {
        _animation = animation;
        _speed = speed;
    }

    public float GetSpeed() => _speed;

    public void Move(Rigidbody rb, Transform transform)
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 input = transform.forward * z + transform.right * x;
        Vector3 direction = input.normalized * _speed * Time.deltaTime;

        rb.MovePosition(rb.position + direction);

        HandleAnimation(input);
    }

    public void HandleAnimation(Vector3 inputDirection)
    {
        if (inputDirection == Vector3.zero)
            _animation.PlayIdle();
        else
            _animation.PlayWalk();
    }
}
