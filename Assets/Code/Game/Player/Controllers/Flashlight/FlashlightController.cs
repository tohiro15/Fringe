using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlashlightController : MonoBehaviour, IFlashlight
{
    [SerializeField] private GameObject _flashlight;

    private InputActionAsset _inputAction;
    private InputAction _flashlightAction;

    private bool _isOn = false;
    public event Action OnFlashlightToggled;

    public void Init(InputActionAsset inputAction, InputAction flashlightAction)
    {
        if (_flashlight == null) return;

        _flashlight.SetActive(_isOn);
        _inputAction = inputAction;
        _flashlightAction = flashlightAction;
    }
    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.F) && _inputAction == null || _flashlightAction.WasPressedThisFrame())
        {
            ToggleFlashlight();
        }
    }

    public void ToggleFlashlight()
    {
        if (_flashlight == null) return;

        _isOn = !_isOn;
        _flashlight.SetActive(_isOn);
        GameManager.Instance.Sound?.PlayFlashlightToggleSound();
        OnFlashlightToggled?.Invoke();
    }
}
