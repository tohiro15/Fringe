using UnityEngine;

public class MouseController : MonoBehaviour, IRotatable
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _sensitivity = 80f;

    private float _xRotation;
    public void Rotate(Rigidbody rb)
    {
        float mouseX = Input.GetAxis("Mouse X") * _sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _sensitivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        _camera.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, mouseX, 0f));
    }

}
