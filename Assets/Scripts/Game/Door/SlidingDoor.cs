using UnityEngine;

public class SlidingDoor : MonoBehaviour, IDoor
{
    [SerializeField] private Vector3 _slideOffset = new(1f, 0f, 0f);
    [SerializeField] private float _moveSpeed = 2f;

    private Vector3 _closedPosition;
    private Vector3 _targetPosition;
    private bool _isOpen = false;

    private void Start()
    {
        _closedPosition = transform.position;
        _targetPosition = _closedPosition;
    }

    public void Interaction()
    {
        _isOpen = !_isOpen;
        _targetPosition = _isOpen ? _closedPosition + _slideOffset : _closedPosition;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * _moveSpeed);
    }
}
