using UnityEngine;
using UnityEngine.InputSystem;

public class CrouchController : MonoBehaviour, ICrouch
{
    [SerializeField] private CapsuleCollider _playerCollider;

    [SerializeField] private Vector3 _crouchCameraPosition;

    [SerializeField] private Transform _headChecker;
    [SerializeField] private float _headCheckRadius = 0.25f;
    [SerializeField] private LayerMask _headObstacleLayer;


    [SerializeField] private float _crouchHeight = 0.5f;
    [SerializeField] private float _crouchRadius = 0.5f;

    private Camera _camera;

    private float _defaultColliderHeight;
    private float _defaultColliderRadius;

    private InputAction _crouchAction;

    private Vector3 _defaultCenter;
    private Vector3 _defaultCameraPosition;

    private IAnimation _animation;
    private bool _isCrouching;
    private bool _wantsToStand;

    public bool GetIsCrouching() => _isCrouching;
    public bool GetWantsToStant() => _wantsToStand;
    public void Init(InputAction crouchAction, Camera playerCamera, IAnimation animation)
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
        if (_crouchAction.WasPressedThisFrame())
        {
            if (!_isCrouching)
                Crouch();
        }

        if (_crouchAction.WasReleasedThisFrame())
        {
            _wantsToStand = true;
        }

        if (_wantsToStand)
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
        if (!CanStandUp())
            return;

        _wantsToStand = false;
        _isCrouching = false;

        _animation?.StopCrouch();
        _playerCollider.height = _defaultColliderHeight;
        _playerCollider.radius = _defaultColliderRadius;
        _playerCollider.center = _defaultCenter;
        _camera.transform.localPosition = _defaultCameraPosition;
    }


    public bool CanStandUp()
    {
        bool result = !Physics.CheckSphere(_headChecker.position, _headCheckRadius, _headObstacleLayer);
        Debug.Log($"CanStandUp: {result}");
        return result;
    }

}
