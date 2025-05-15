using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private MouseController _mouseController;

    [SerializeField] private Rigidbody _rb;

    [SerializeField] private float gravityMultiplier = 2f;

    private IMovementStrategy _currentStategy;
    private IMovementStrategy _walkStrategy;
    private IMovementStrategy _runStrategy;

    private IRotatable _rotatable;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _walkStrategy = new WalkMovement(5f);
        _runStrategy = new RunMovement(8f);
        _currentStategy = _walkStrategy;

        _rotatable = _mouseController;
    }

    private void Update()
    {
        _currentStategy = Input.GetKey(KeyCode.LeftShift) ? _runStrategy : _walkStrategy;
        _rotatable?.HandleInput();
    }

    private void FixedUpdate()
    {
        Vector3 extraGravity = Physics.gravity * (gravityMultiplier - 1);
        _rb.AddForce(extraGravity, ForceMode.Acceleration);

        _currentStategy?.Move(_rb, transform);
        _rotatable?.Rotate(_rb);
    }

}
