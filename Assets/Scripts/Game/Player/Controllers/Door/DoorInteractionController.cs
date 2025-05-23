using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorInteractionController : MonoBehaviour, IDoor
{
    [SerializeField] private float _interactDistance = 1.5f;

    private Camera _playerCamera;

    private InputActionAsset _inputAction;
    private InputAction _interactAction;

    public void Init(InputActionAsset inputAction, InputAction interactAction,  Camera playerCamera)
    {
        _inputAction = inputAction;
        _interactAction = interactAction;
        _playerCamera = playerCamera;
    }

    public void FindDoor()
    {
        Ray ray = new Ray(_playerCamera.transform.position, _playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, _interactDistance))
        {
            Transform hitTransform = hit.transform;

            if (hitTransform.CompareTag("Door"))
            {
                UIManager.Instance.UpdateInteractionText(true);

                if (Input.GetKeyDown(KeyCode.E) && _inputAction == null || _interactAction.WasPressedThisFrame())
                {
                    Door door = hitTransform.GetComponent<Door>();
                    door?.Interact();
                }

                return;
            }
        }

        UIManager.Instance.UpdateInteractionText(false);
    }
}
