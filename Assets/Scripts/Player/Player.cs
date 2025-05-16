using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private MouseController _mouseController;
    [SerializeField] private FlashlightController _flashController;

    [SerializeField] private Rigidbody _rb;

    [SerializeField] private float gravityMultiplier = 2f;

    private IMovementStrategy _currentStategy;
    private IMovementStrategy _walkStrategy;
    private IMovementStrategy _runStrategy;

    private IRotatable _rotatable;
    private IFlashlight _flashlight;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _walkStrategy = new WalkMovement(4f);
        _runStrategy = new RunMovement(6f);
        _currentStategy = _walkStrategy;

        _rotatable = _mouseController;
        _flashlight = _flashController;
    }

    private void Update()
    {
        _currentStategy = Input.GetKey(KeyCode.LeftShift) ? _runStrategy : _walkStrategy;

        _rotatable?.HandleInput();

        if (Input.GetKeyDown(KeyCode.F))
        {
            _flashlight?.ToggleFlashlight();
        }
    }

    private void FixedUpdate()
    {
        Vector3 extraGravity = Physics.gravity * (gravityMultiplier - 1);
        _rb.AddForce(extraGravity, ForceMode.Acceleration);

        _currentStategy?.Move(_rb, transform);
        _rotatable?.Rotate(_rb);
    }

}
