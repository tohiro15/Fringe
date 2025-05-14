using UnityEngine;

public class MouseController : MonoBehaviour, IRotatable
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _sensitivity = 200f;

    [SerializeField] private float _maxX = 90f;
    [SerializeField] private float _minX = -90f;

    private float _xRotation;
    public void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * _sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * _sensitivity;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, _minX, _maxX);

        _camera.gameObject.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);

        transform.Rotate(Vector3.up * mouseX);
    }
}
