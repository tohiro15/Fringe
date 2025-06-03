using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class FlashlightController : MonoBehaviour, IFlashlight
{
    [SerializeField] private GameObject _flashlightGO;

    private InputAction _toggleAction;
    private bool _isOn;

    public event Action<bool> OnFlashlightToggled;

    public void Init(InputAction toggleAction, GameObject flashlightGO)
    {
        _toggleAction = toggleAction;
        _flashlightGO = flashlightGO;
        _flashlightGO.SetActive(false);
        _isOn = false;
    }

    public void HandleInput()
    {
        if (_toggleAction.WasPressedThisFrame())
        {
            _isOn = !_isOn;
            _flashlightGO.SetActive(_isOn);
            GameManager.Instance.Sound?.PlayFlashlightToggleSound();
            OnFlashlightToggled?.Invoke(_isOn);
        }
    }
}
