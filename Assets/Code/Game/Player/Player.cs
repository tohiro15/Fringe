using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private InputAction _moveAction;
    private InputAction _lookAction;
    private InputAction _jumpAction;
    private InputAction _sprintAction;
    private InputAction _crouchAction;
    private InputAction _interactAction;
    private InputAction _flashlightAction;
    private InputAction _pauseActionPlayer;

    //Controllers

    private MouseController _mouseController;
    private JumpController _jumpController;
    private CrouchController _crouchController;
    private FlashlightController _flashlightController;
    private AnimationController _animationController;
    private DoorInteractionController _doorInteractionController;

    [Header("Settings")]
    [Space]

    [Header("Movement")]

    [SerializeField] private float _walkSpeed = 2f;
    [SerializeField] private float _runSpeed = 5f;

    [Header("Jump")]

    [SerializeField] private float _jumpForce = 5f;

    [Header("Door Interaction")]

    [SerializeField] private float _doorInteractDistance = 1.5f;

    [Header("Components")]
    [Space]

    [SerializeField] private Rigidbody _rb;

    private IMovementStrategy _currentStrategy ;
    private IMovementStrategy _walkStrategy;
    private IMovementStrategy _runStrategy;

    private IRotatable _rotatable;
    private IJump _jump;
    private ICrouch _crouch;
    private IFlashlight _flashlight;
    private IAnimation _animation;
    private IDoorController _doorController;

    private void OnEnable()
    {
        SettingsManager.Instance.GetInputActionsAsset().FindActionMap("Player")?.Enable();
    }

    private void OnDisable()
    {
        SettingsManager.Instance.GetInputActionsAsset().FindActionMap("Player")?.Disable();
    }

    private void Awake()
    {
        _mouseController = GetComponent<MouseController>();
        _jumpController = GetComponent<JumpController>();
        _crouchController = GetComponent<CrouchController>();
        _flashlightController = GetComponent<FlashlightController>();
        _animationController = GetComponent<AnimationController>();
        _doorInteractionController = GetComponent<DoorInteractionController>();

        _moveAction = SettingsManager.Instance.GetAction("Player", "Move");
        _lookAction = SettingsManager.Instance.GetAction("Player", "Look");
        _crouchAction = SettingsManager.Instance.GetAction("Player", "Crouch");
        _jumpAction = SettingsManager.Instance.GetAction("Player", "Jump");
        _sprintAction = SettingsManager.Instance.GetAction("Player", "Sprint");
        _interactAction = SettingsManager.Instance.GetAction("Player", "Interact");
        _flashlightAction = SettingsManager.Instance.GetAction("Player", "Flashlight");
        _pauseActionPlayer = SettingsManager.Instance.GetAction("Player", "Pause");
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _rotatable = _mouseController;
        _jump = _jumpController;
        _crouch = _crouchController;
        _flashlight = _flashlightController;
        _animation = _animationController;
        _doorController = _doorInteractionController;

        _walkStrategy = new WalkMovement(_moveAction,  _animation, _rb, _walkSpeed);
        _runStrategy = new RunMovement(_moveAction, _animation, _rb, _runSpeed);
        _currentStrategy  = _walkStrategy;

        _rotatable?.Init(_lookAction);
        _jump?.Init(_jumpAction);
        _crouch?.Init(_crouchAction, _mouseController.GetCamera(), _animation);
        _flashlight?.Init(_flashlightAction);
        _doorController?.Init(_interactAction, _mouseController.GetCamera());

    }

    private void Update()
    {
        HandlePauseInput();

        if (GameManager.Instance.CurrentState != GameState.Playing) return;

        _crouch?.HandleInput();

        if (_crouch != null && !_crouch.GetIsCrouching())
        {
            if (_sprintAction.IsPressed())
            {
                _currentStrategy = _runStrategy;
            }
            else
            {
                _currentStrategy = _walkStrategy;
            }
        }
        else
        {
            _currentStrategy = _walkStrategy;
        }


        _rotatable?.HandleInput();

        if (_crouch != null && !_crouch.GetIsCrouching())
        {
            _jump?.HandleInput(_rb, _jumpForce);
        }

        _flashlight?.HandleInput();

        _doorController?.FindDoorAndInteract(_doorInteractDistance);
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.CurrentState != GameState.Playing) return;

        Vector3 extraGravity = Physics.gravity * (2f - 1);
        _rb.AddForce(extraGravity, ForceMode.Acceleration);

        _jump?.CheckGround();

        _currentStrategy ?.Move(transform);
        _rotatable?.Rotate(_rb);
    }

    private void HandlePauseInput()
    {
        if (_pauseActionPlayer.WasPressedThisFrame())
        {
            if (GameManager.Instance.CurrentState == GameState.Playing)
            {
                UIManager.Instance.OpenPauseMenu();
            }
            else if (GameManager.Instance.CurrentState == GameState.Pause)
            {
                UIManager.Instance.ClosePauseMenu();
            }
        }
    }

}
