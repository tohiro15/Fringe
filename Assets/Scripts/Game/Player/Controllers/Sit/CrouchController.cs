using UnityEngine;
using UnityEngine.InputSystem;

public class CrouchController : MonoBehaviour, ICrouch
{
    [SerializeField] private CapsuleCollider _playerCollider;

    [SerializeField] private Vector3 _crouchCameraPosition;

    [SerializeField] private float _rayStandupDistance = 1f;

    [SerializeField] private float _crouchHeight = 0.5f;
    [SerializeField] private float _crouchRadius = 0.5f;

    private Camera _camera;

    private float _defaultColliderHeight;
    private float _defaultColliderRadius;

    private InputActionAsset _inputAction;
    private InputAction _crouchAction;

    private Vector3 _defaultCenter;
    private Vector3 _defaultCameraPosition;

    private IAnimation _animation;
    private bool _isCrouching;
    private bool _wantsToStand;

    public bool GetIsCrouching() => _isCrouching;
    public bool GetWantsToStant() => _wantsToStand;
    public void Init(InputActionAsset inputAction, InputAction crouchAction, Camera playerCamera, IAnimation animation)
    {
        if (_playerCollider != null)
        {
            _defaultColliderHeight = _playerCollider.height;
            _defaultColliderRadius = _playerCollider.radius;
            _defaultCenter = _playerCollider.center;
        }

        _crouchAction = crouchAction;

        _camera = playerCamera;
        _defaultCameraPosition = _camera.transform.localPosition;

        _animation = animation;
    }
    public void HandleInput()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl) && !_isCrouching && _inputAction == null || _crouchAction.WasPerformedThisFrame())
        {
            Crouch();
        }
        else if(Input.GetKeyUp(KeyCode.LeftControl) && _isCrouching && _inputAction == null || _crouchAction.WasReleasedThisFrame())
        {
            _isCrouching = false;
            _wantsToStand = true;
        }

        if(_wantsToStand && !_isCrouching)
        {
            StandUp();
        }
    }

    public void Crouch()
    {
        _isCrouching = true;
        _wantsToStand = false;

        _animation?.PlayIdleCrouch();
        _playerCollider.height =  _crouchHeight;
        _playerCollider.radius = _crouchRadius;
        _playerCollider.center = new Vector3(0, _crouchRadius, 0);
        _camera.transform.localPosition = _crouchCameraPosition;
    }

    public void StandUp()
    {
        if (Physics.Raycast(transform.position, Vector3.up, _rayStandupDistance)) return;

        _wantsToStand = false;

        _animation?.StopCrouch();
        _playerCollider.height = _defaultColliderHeight;
        _playerCollider.radius = _defaultColliderRadius;
        _playerCollider.center = _defaultCenter;
        _camera.transform.localPosition = _defaultCameraPosition;
    }

}
