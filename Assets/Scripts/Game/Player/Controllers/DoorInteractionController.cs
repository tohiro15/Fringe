using UnityEngine;
using UnityEngine.InputSystem;

public class DoorInteractionController : MonoBehaviour, IDoorController
{
    [SerializeField] private float _interactDistance = 2f;

    private InputAction _interactAction;
    private Camera _playerCamera;
    private System.Action<bool> _showPrompt;

    public void Init(InputAction interactAction, Camera playerCamera, float interactDistance, System.Action<bool> showPrompt)
    {
        _interactAction = interactAction;
        _playerCamera = playerCamera;
        _interactDistance = interactDistance;
        _showPrompt = showPrompt;
    }

    public void HandleInteraction()
    {
        Ray ray = new Ray(_playerCamera.transform.position, _playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, _interactDistance))
        {
            IDoor door = hit.transform.GetComponentInParent<IDoor>();
            if (door != null)
            {
                _showPrompt?.Invoke(true);
                if (_interactAction.WasPressedThisFrame())
                    door.Interaction();
                return;
            }
        }
        _showPrompt?.Invoke(false);
    }
}
