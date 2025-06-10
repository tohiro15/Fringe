using UnityEngine;
using UnityEngine.InputSystem;

public class DoorInteractionController : MonoBehaviour, IDoorController
{
    private Camera _playerCamera;
    private InputAction _interactAction;

    public void Init(InputAction interactAction, Camera playerCamera)
    {
        _interactAction = interactAction;
        _playerCamera = playerCamera;
    }

    public void FindDoorAndInteract(float interactDistance)
    {
        Ray ray = new(_playerCamera.transform.position, _playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            Transform hitTransform = hit.transform;

            IDoor door = hitTransform.GetComponentInParent<IDoor>();

            if (door != null)
            {
                UIManager.Instance.UpdateInteractionImage(true);

                if ((_interactAction != null && _interactAction.WasPressedThisFrame()) || Input.GetKeyDown(KeyCode.E))
                {
                    door.Interaction();
                }

                return;
            }
        }
        UIManager.Instance.UpdateInteractionImage(false);
    }

}
