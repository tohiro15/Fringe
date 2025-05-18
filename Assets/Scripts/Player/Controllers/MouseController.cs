using UnityEngine;

public class MouseController : MonoBehaviour, IRotatable
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _sensitivity = 80f;

    private float _xRotation;

    private float _mouseX;
    private float _mouseY;
    public void HandleInput()
    {
        if (GameManager.Instance.CurrentState != GameState.Playing) return;

        _mouseX = Input.GetAxis("Mouse X") * _sensitivity * Time.fixedDeltaTime;
        _mouseY = Input.GetAxis("Mouse Y") * _sensitivity * Time.fixedDeltaTime;
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
