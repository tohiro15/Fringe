using UnityEngine;
using UnityEngine.InputSystem;

public class DoorInteractionController : MonoBehaviour, IDoorController
{
    [SerializeField] private float _interactDistance = 1.5f;
    private Camera _playerCamera;
    private InputAction _interactAction;

    public void Init(InputAction interactAction, Camera playerCamera)
    {
        _interactAction = interactAction;
        _playerCamera = playerCamera;
    }

    public void FindDoorAndInteract()
    {
        Ray ray = new(_playerCamera.transform.position, _playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, _interactDistance))
        {
            Transform hitTransform = hit.transform;

            // »щем родител€, который реализует IDoor
            IDoor door = hitTransform.GetComponentInParent<IDoor>();

            if (door != null)
            {
                UIManager.Instance.UpdateInteractionText(true);

                if ((_interactAction != null && _interactAction.WasPressedThisFrame()) || Input.GetKeyDown(KeyCode.E))
                {
                    door.Interaction();
                }

                return;
            }
        }
        UIManager.Instance.UpdateInteractionText(false);
    }

}
