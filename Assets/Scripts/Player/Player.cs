using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private MouseController _mouseController;
    [SerializeField] private JumpController _jumpController;
    [SerializeField] private FlashlightController _flashlightController;
    [SerializeField] private AnimationController _animationController;

    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Animator _playerAnimator;

    private IMovementStrategy _currentStategy;
    private IMovementStrategy _walkStrategy;
    private IMovementStrategy _runStrategy;

    private IRotatable _rotatable;
    private IJump _jump;
    private IFlashlight _flashlight;
    private IAnimation _animation;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _rotatable = _mouseController;
        _jump = _jumpController;
        _flashlight = _flashlightController;
        _animation = _animationController;

        _walkStrategy = new WalkMovement(_animation, 2f);
        _runStrategy = new RunMovement(_animation, 5f);
        _currentStategy = _walkStrategy;

    }

    private void Update()
    {
        if (GameManager.Instance.CurrentState != GameState.Playing) return;

        _currentStategy = Input.GetKey(KeyCode.LeftShift) ? _runStrategy : _walkStrategy;

        _rotatable?.HandleInput();

        _jump?.HandleInput(_rb);

        _flashlight?.HandleInput();

    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.CurrentState != GameState.Playing) return;

        Vector3 extraGravity = Physics.gravity * (2f - 1);
        _rb.AddForce(extraGravity, ForceMode.Acceleration);

        _jump?.CheckGround();

        _currentStategy?.Move(_rb, transform);
        _rotatable?.Rotate(_rb);
    }

}
