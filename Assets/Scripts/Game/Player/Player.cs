// Player.cs
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [Header("Controllers")]
    [SerializeField] private MouseController _mouseController;
    [SerializeField] private JumpController _jumpController;
    [SerializeField] private CrouchController _crouchController;
    [SerializeField] private FlashlightController _flashlightController;
    [SerializeField] private AnimationController _animationController;
    [SerializeField] private DoorInteractionController _doorInteractionController;

    [Header("Movement Settings")]
    [SerializeField] private float _walkSpeed = 2f;
    [SerializeField] private float _runSpeed = 5f;

    private Rigidbody _rb;
    private IMovementStrategy _walkStrategy;
    private IMovementStrategy _runStrategy;
    private IMovementStrategy _currentStrategy;

    private IRotatable _rotatable;
    private IJump _jump;
    private ICrouch _crouch;
    private IFlashlight _flashlight;
    private IAnimation _animation;
    private IDoorController _doorController;
    private ISettings _settings => SettingsManager.Instance;
    private IUI _ui => UIManager.Instance;

    private InputAction _moveAction;
    private InputAction _lookAction;
    private InputAction _jumpAction;
    private InputAction _sprintAction;
    private InputAction _crouchAction;
    private InputAction _interactAction;
    private InputAction _flashlightAction;
    private InputAction _pauseAction;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        var map = _settings.InputActions.FindActionMap("Player");
        _moveAction = map.FindAction("Move");
        _lookAction = map.FindAction("Look");
        _jumpAction = map.FindAction("Jump");
        _sprintAction = map.FindAction("Sprint");
        _crouchAction = map.FindAction("Crouch");
        _interactAction = map.FindAction("Interact");
        _flashlightAction = map.FindAction("Flashlight");
        _pauseAction = map.FindAction("Pause");

        _rotatable = _mouseController;
        _rotatable.Init(_lookAction, _settings.Sensitivity);

        _jump = _jumpController;
        _jump.Init(_jumpAction, 5f, _jumpController.GroundCheck, 0.4f);

        _crouch = _crouchController;
        _crouch.Init(_crouchAction, _crouchController.PlayerCollider, _crouchController.HeadChecker,
                     _crouchController.HeadCheckRadius, _crouchController.CrouchCameraOffset, _animationController);

        _flashlight = _flashlightController;
        _flashlight.Init(_flashlightAction, _flashlightController.FlashlightGO);

        _doorController = _doorInteractionController;
        _doorController.Init(_interactAction, _mouseController.Camera, _doorInteractionController.InteractDistance,
                             show => _ui.ShowInteractPrompt(show));

        _animation = _animationController;
        _walkStrategy = new MovementStrategy(_moveAction, _animation, _rb, _walkSpeed, MovementState.Walk);
        _runStrategy = new MovementStrategy(_moveAction, _animation, _rb, _runSpeed, MovementState.Run);
        _currentStrategy = _walkStrategy;
    }

    private void OnEnable()
    {
        // ¬ключаем карту "Player" дл€ Input System
        _settings.InputActions.FindActionMap("Player")?.Enable();
    }

    private void OnDisable()
    {
        _settings.InputActions.FindActionMap("Player")?.Disable();
    }

    private void Update()
    {
        HandlePauseToggle();

        if (GameManager.Instance.CurrentState != GameState.Playing) return;

        HandleMovementSwitch();
        _rotatable.HandleInput();
        _jump.HandleInput(_rb);
        _crouch.HandleInput();
        _flashlight.HandleInput();
        _doorController.HandleInteraction();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.CurrentState != GameState.Playing) return;

        _jump.CheckGround();
        _currentStrategy.Move(transform);
        _rotatable.Rotate(_rb);
    }

    private void HandlePauseToggle()
    {
        if (_pauseAction.WasPressedThisFrame())
        {
            if (GameManager.Instance.CurrentState == GameState.Playing)
                _ui.OpenPauseMenu();
            else if (GameManager.Instance.CurrentState == GameState.Pause)
                _ui.ClosePauseMenu();
        }
    }

    private void HandleMovementSwitch()
    {
        // ≈сли персонаж в приседе Ч только ходьба
        if (_crouch.IsCrouching)
        {
            _currentStrategy = _walkStrategy;
            return;
        }

        // ≈сли удерживаетс€ shift или нажато действие Sprint Ч бег
        bool sprintPressed = _sprintAction.ReadValue<float>() > 0.1f;
        _currentStrategy = sprintPressed ? _runStrategy : _walkStrategy;
    }
}
