using UnityEngine;
using UnityEngine.InputSystem;

public class JumpController : MonoBehaviour, IJump
{
    [SerializeField] private float _jumpForce = 5f;
    public Transform GroundCheck => _groundCheck;

    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundDistance = 0.4f;

    private InputAction _jumpAction;
    private bool _isGrounded;

    public void Init(InputAction jumpAction, float force, Transform groundCheck, float groundDistance)
    {
        _jumpAction = jumpAction;
        _jumpForce = force;
        _groundCheck = groundCheck;
        _groundDistance = groundDistance;
    }

    public void CheckGround()
    {
        _isGrounded = Physics.Raycast(_groundCheck.position, Vector3.down, _groundDistance);
    }

    public void HandleInput(Rigidbody rb)
    {
        if (_jumpAction.WasPressedThisFrame() && _isGrounded)
        {
            rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }
}
