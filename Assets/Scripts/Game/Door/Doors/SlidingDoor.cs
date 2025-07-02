using UnityEngine;

public class SlidingDoor : DoorBase
{

    [SerializeField] private Vector3 _slideOffset = new(1f, 0f, 0f);
    [SerializeField] private float _moveSpeed = 2f;

    private Vector3 _closedPosition;
    private Vector3 _targetPosition;

    private void Start()
    {
        _closedPosition = transform.position;
        _targetPosition = _closedPosition;

        _type = DoorType.SlidingDoor;
    }

    public override void Interaction()
    {
        _isOpen = !_isOpen;
        _targetPosition = _isOpen ? _closedPosition + _slideOffset : _closedPosition;

        SoundManager.Instance.PlayDoorSound(_type, _isOpen, _audioSource);
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * _moveSpeed);
    }
}
