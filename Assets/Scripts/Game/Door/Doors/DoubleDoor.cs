using UnityEngine;

public class DoubleDoor : DoorBase
{
    [SerializeField] private DoorType type = DoorType.DoubleDoor;

    [SerializeField] private Transform _leftDoor;
    [SerializeField] private Transform _rightDoor;
    [SerializeField] private float _openAngle = 90f;
    [SerializeField] private float _openSpeed = 5f;

    private Quaternion _leftClosed, _rightClosed;
    private Quaternion _leftTarget, _rightTarget;

    private void Start()
    {
        _leftClosed = _leftDoor.rotation;
        _rightClosed = _rightDoor.rotation;
        _leftTarget = _leftClosed;
        _rightTarget = _rightClosed;
    }
    private void Update()
    {
        _leftDoor.rotation = Quaternion.Lerp(_leftDoor.rotation, _leftTarget, Time.deltaTime * _openSpeed);
        _rightDoor.rotation = Quaternion.Lerp(_rightDoor.rotation, _rightTarget, Time.deltaTime * _openSpeed);
    }

    public override void Interaction()
    {
        _isOpen = !_isOpen;
        float angle = _isOpen ? _openAngle : 0f;
        _leftTarget = Quaternion.Euler(0, -angle, 0) * _leftClosed;
        _rightTarget = Quaternion.Euler(0, angle, 0) * _rightClosed;

        SoundManager.Instance.PlayDoorSound(type, _isOpen, _audioSource);
    }

}
