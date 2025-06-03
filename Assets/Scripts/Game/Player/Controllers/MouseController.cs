using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))]
public class MouseController : MonoBehaviour, IRotatable
{
    [SerializeField] private float _maxVerticalAngle = 90f;

    public Camera Camera => _camera;
    private Camera _camera;

    private InputAction _lookAction;
    private float _sensitivity;
    private float _xRotation = 0f;

    private Vector2 _lookValue;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    public void Init(InputAction lookAction, float sensitivity)
    {
        _lookAction = lookAction;
        _sensitivity = sensitivity;
    }

    public void HandleInput()
    {
        _lookValue = _lookAction.ReadValue<Vector2>();
    }

    public void Rotate(Rigidbody rb)
    {
        float mouseX = _lookValue.x * _sensitivity * Time.deltaTime;
        float mouseY = _lookValue.y * _sensitivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -_maxVerticalAngle, _maxVerticalAngle);

        _camera.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, mouseX, 0f));
    }
}
