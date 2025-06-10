using UnityEngine;
using UnityEngine.InputSystem;

public class JumpController : MonoBehaviour, IJump
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundDistance = 0.4f;

    private InputActionAsset _inputAction;
    private InputAction _jumpAction;

    private bool _isGrounded;

    private void OnDrawGizmosSelected()
    {
        if (_groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_groundCheck.position, _groundDistance);
        }
    }

    public void Init(InputActionAsset inputAction, InputAction jumpAction)
    {
        _inputAction = inputAction;
        _jumpAction = jumpAction;
    }

    public void CheckGround()
    {
        _isGrounded = Physics.Raycast(_groundCheck.position, Vector3.down, _groundDistance);
        Debug.Log($"IsGrounded: {_isGrounded}");
    }

    public void HandleInput(Rigidbody rb, float jumpForce)
    {
        if (_inputAction == null)
        {
            if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
        else
        {
            if (_jumpAction.WasPressedThisFrame() && _isGrounded)
            {
                    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }
}
