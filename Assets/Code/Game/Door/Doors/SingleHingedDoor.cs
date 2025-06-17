using UnityEngine;

public class SingleHingedDoor : DoorBase
{
    [SerializeField] private float _openAngle = 90f;
    [SerializeField] private float _openSpeed = 10f;

    private Quaternion _closedRotation;
    private Quaternion _targetRotation;

    private void Start()
    {
        _type = DoorType.SingleHindedDoor;
        _closedRotation = transform.rotation;
        _targetRotation = _closedRotation;
    }

    public override void Interaction()
    {
        _isOpen = !_isOpen;
        float angle = _isOpen ? _openAngle : 0f;
        _targetRotation = Quaternion.Euler(0, angle, 0) * _closedRotation;

        SoundManager.Instance.PlayDoorSound(_type, _isOpen, _audioSource);
    }

    private void Update()
    {
        transform.rotation = Quaternion.LerpUnclamped(transform.rotation, _targetRotation, Time.deltaTime * _openSpeed);
    }
}
