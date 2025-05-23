using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    [SerializeField] private InputActionAsset _inputAction;

    private InputAction _moveAction;
    private InputAction _lookAction;
    private InputAction _jumpAction;
    private InputAction _sprintAction;
    private InputAction _crouchAction;
    private InputAction _interactAction;
    private InputAction _flashlightAction;
    private InputAction _pauseActionPlayer;

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

    private void OnEnable()
    {
        _inputAction.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        _inputAction.FindActionMap("Player").Disable();
    }
    private void Awake()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        _lookAction = InputSystem.actions.FindAction("Look");
        _crouchAction = InputSystem.actions.FindAction("Crouch");
        _jumpAction = InputSystem.actions.FindAction("Jump");
        _sprintAction = InputSystem.actions.FindAction("Sprint");
        _interactAction = InputSystem.actions.FindAction("Interact");
        _flashlightAction = InputSystem.actions.FindAction("Flashlight");
        _pauseActionPlayer = InputSystem.actions.FindAction("Pause");
    }
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _rotatable = _mouseController;
        _jump = _jumpController;
        _crouch = _crouchController;
        _flashlight = _flashlightController;
        _animation = _animationController;
        _door = _doorInteractionController;

        _walkStrategy = new WalkMovement(_inputAction, _moveAction,  _animation, _rb, 2f);
        _runStrategy = new RunMovement(_inputAction, _moveAction, _animation, _rb, 5f);
        _currentStategy = _walkStrategy;

        _rotatable?.Init(_inputAction, _lookAction);
        _jump?.Init(_inputAction, _jumpAction);
        _crouch?.Init(_inputAction, _crouchAction, _mouseController.GetCamera(), _animation);
        _flashlight?.Init(_inputAction, _flashlightAction);
        _door?.Init(_inputAction, _interactAction, _mouseController.GetCamera());

    }

    private void Update()
    {
        if (GameManager.Instance.CurrentState != GameState.Playing) return;

        if (_crouch != null && _crouch.GetWantsToStant())
        {
            _currentStategy = _walkStrategy;
        }
        else if (Input.GetKey(KeyCode.LeftShift) && _inputAction == null || _sprintAction.IsPressed())
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

        if (/*Input.GetKeyDown(KeyCode.Escape) && _inputAction == null ||*/ _pauseActionPlayer.WasPressedThisFrame()) SceneManager.LoadScene(0); // временный способ выйти в главное меню
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.CurrentState != GameState.Playing) return;

        Vector3 extraGravity = Physics.gravity * (2f - 1);
        _rb.AddForce(extraGravity, ForceMode.Acceleration);

        _jump?.CheckGround();

        _currentStategy?.Move(transform);
        _rotatable?.Rotate(_rb);
    }

}
