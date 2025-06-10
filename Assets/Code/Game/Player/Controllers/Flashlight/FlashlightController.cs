using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlashlightController : MonoBehaviour, IFlashlight
{
    [SerializeField] private GameObject _flashlight;

    private InputAction _flashlightAction;

    private bool _isOn = false;
    public event Action OnFlashlightToggled;

    public void Init(InputAction flashlightAction)
    {
        if (_flashlight == null) return;

        _flashlight.SetActive(_isOn);
        _flashlightAction = flashlightAction;
    }
    public void HandleInput()
    {
        if (_flashlightAction.WasPressedThisFrame())
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
