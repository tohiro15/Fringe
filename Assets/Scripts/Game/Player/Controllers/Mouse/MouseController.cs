using UnityEngine;
using UnityEngine.InputSystem;

public class MouseController : MonoBehaviour, IRotatable
{
    [SerializeField] private Camera _camera;

    private InputAction _lookAction;

    private float _xRotation;

    private float _mouseX;
    private float _mouseY;

    private Vector2 _lookAmt;
    public Camera GetCamera() => _camera;

    public void Init(InputAction lookAction)
    {
        _lookAction = lookAction;
    }

    public void HandleInput()
    {
        if (GameManager.Instance.CurrentState != GameState.Playing) return;

        _lookAmt = _lookAction.ReadValue<Vector2>();

        //_mouseX = _lookAmt.x * SettingsManager.Instance.GetSensitivity() * Time.deltaTime;
        //_mouseY = _lookAmt.y * SettingsManager.Instance.GetSensitivity() * Time.deltaTime;

        _mouseX = _lookAmt.x * 25 * Time.deltaTime;
        _mouseY = _lookAmt.y * 25 * Time.deltaTime;
    }


    public void Rotate(Rigidbody rb)
    {
        if (GameManager.Instance.CurrentState != GameState.Playing) return;

        _xRotation -= _mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        _camera.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, _mouseX, 0f));
    }
}
