// CrouchController.cs
using UnityEngine;
using UnityEngine.InputSystem;

public class CrouchController : MonoBehaviour, ICrouch
{
    [SerializeField] private CapsuleCollider _playerCollider;
    [Header("Crouch Settings")]
    [SerializeField] private Vector3 _crouchCameraOffset = new Vector3(0f, 0.5f, 0f);
    [SerializeField] private Transform _headChecker;
    [SerializeField] private float _headCheckRadius = 0.25f;

    private InputAction _crouchAction;
    private Camera _playerCamera;
    private IAnimation _animation;

    private Vector3 _originalCameraPos;
    private float _originalHeight;
    private float _originalRadius;
    private Vector3 _originalCenter;

    public bool IsCrouching { get; private set; }

    public void Init(InputAction crouchAction, CapsuleCollider playerCollider, Transform headChecker, float headCheckRadius, Vector3 crouchCameraOffset, IAnimation anim)
    {
        _crouchAction = crouchAction;
        _playerCollider = playerCollider;
        _headChecker = headChecker;
        _headCheckRadius = headCheckRadius;
        _crouchCameraOffset = crouchCameraOffset;
        _animation = anim;

        _originalHeight = _playerCollider.height;
        _originalRadius = _playerCollider.radius;
        _originalCenter = _playerCollider.center;

        _playerCamera = Camera.main;
        _originalCameraPos = _playerCamera.transform.localPosition;
    }

    public void HandleInput()
    {
        if (_crouchAction.WasPressedThisFrame())
        {
            if (!IsCrouching)
                StartCrouch();
            else if (CanStandUp())
                EndCrouch();
        }
    }

    private void StartCrouch()
    {
        IsCrouching = true;
        _animation.PlayCrouchStart();

        _playerCollider.height = _originalHeight * 0.5f;
        _playerCollider.radius = _originalRadius * 0.8f;
        _playerCollider.center = new Vector3(_originalCenter.x, _originalCenter.y * 0.5f, _originalCenter.z);

        _playerCamera.transform.localPosition = _crouchCameraOffset;
    }

    private void EndCrouch()
    {
        IsCrouching = false;
        _animation.PlayCrouchEnd();

        _playerCollider.height = _originalHeight;
        _playerCollider.radius = _originalRadius;
        _playerCollider.center = _originalCenter;

        _playerCamera.transform.localPosition = _originalCameraPos;
    }

    public bool CanStandUp()
    {
        return !Physics.CheckSphere(_headChecker.position, _headCheckRadius, LayerMask.GetMask("Default"));
        // layerMask УDefaultФ замените на тот слой, где лежат преп€тстви€
    }
}
