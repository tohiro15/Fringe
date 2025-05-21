using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private MouseController _mouseController;
    [SerializeField] private JumpController _jumpController;
    [SerializeField] private CrouchController _crouchController;
    [SerializeField] private FlashlightController _flashlightController;
    [SerializeField] private AnimationController _animationController;
    [SerializeField] private DoorInteractionController _doorInteractionController;

    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Animator _playerAnimator;

    private IMovementStrategy _currentStategy;
    private IMovementStrategy _walkStrategy;
    private IMovementStrategy _runStrategy;

    private IRotatable _rotatable;
    private IJump _jump;
    private ICrouch _crouch;
    private IFlashlight _flashlight;
    private IAnimation _animation;
    private IDoor _door;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _rotatable = _mouseController;
        _jump = _jumpController;
        _crouch = _crouchController;
        _flashlight = _flashlightController;
        _animation = _animationController;
        _door = _doorInteractionController;

        _walkStrategy = new WalkMovement(_animation, 2f);
        _runStrategy = new RunMovement(_animation, 5f);
        _currentStategy = _walkStrategy;

        _crouch?.Init(_mouseController.GetCamera(), _animation);
        _door?.Init(_mouseController.GetCamera());

    }

    private void Update()
    {
        if (GameManager.Instance.CurrentState != GameState.Playing) return;

        if (_crouch != null && _crouch.GetWantsToStant())
        {
            _currentStategy = _walkStrategy;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            _currentStategy = _runStrategy;
        }
        else
        {
            _currentStategy = _walkStrategy;
        }


        _rotatable?.HandleInput();

        _jump?.HandleInput(_rb);

        _crouch?.HandleInput();

        _flashlight?.HandleInput();

        _door?.FindDoor();
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
