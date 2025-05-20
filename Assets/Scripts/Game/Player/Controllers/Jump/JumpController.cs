using UnityEngine;

public class JumpController : MonoBehaviour, IJump
{
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundDistance = 0.4f;

    private bool _isGrounded;

    private void OnDrawGizmosSelected()
    {
        if (_groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_groundCheck.position, _groundDistance);
        }
    }

    public void CheckGround()
    {
        _isGrounded = Physics.Raycast(_groundCheck.position, Vector3.down, _groundDistance);
    }

    public void HandleInput(Rigidbody rb)
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }

    }
}
