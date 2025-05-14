using UnityEngine;

public class RunMovement : IMovementStrategy
{
    private float _speed;

    public RunMovement(float speed)
    {
        _speed = speed;
    }

    public void Move(Rigidbody rb, Transform transform)
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 direction = (transform.forward * z + transform.right * x) * _speed * Time.deltaTime;
        rb.MovePosition(rb.position + direction);
    }
}
