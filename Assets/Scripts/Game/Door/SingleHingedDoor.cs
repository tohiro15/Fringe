using UnityEngine;

public class SingleHingedDoor : MonoBehaviour, IDoor
{
    [SerializeField] private float _openAngle = 90f;
    [SerializeField] private float _openSpeed = 5f;

    private Quaternion _closedRotation;
    private Quaternion _targetRotation;
    private bool _isOpen = false;

    private void Start()
    {
        _closedRotation = transform.rotation;
        _targetRotation = _closedRotation;
    }

    public void Interaction()
    {
        _isOpen = !_isOpen;
        float angle = _isOpen ? _openAngle : 0f;
        _targetRotation = Quaternion.Euler(0, angle, 0) * _closedRotation;
    }

    private void Update()
    {
        transform.rotation = Quaternion.LerpUnclamped(transform.rotation, _targetRotation, Time.deltaTime * _openSpeed);
    }
}
