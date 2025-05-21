using TMPro;
using UnityEngine;

public class DoorInteractionController : MonoBehaviour, IDoor
{
    [SerializeField] private float _interactDistance = 1.5f;

    private Camera _playerCamera;

    public void Init(Camera playerCamera)
    {
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

                if (Input.GetKeyDown(KeyCode.E))
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
