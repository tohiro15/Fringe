using System;
using UnityEngine;

public class FlashlightController : MonoBehaviour, IFlashlight
{
    [SerializeField] private GameObject _flashlight;

    private bool _isOn = false;
    public event Action OnFlashlightToggled;

    private void Start()
    {
        if (_flashlight == null) return;

        _flashlight.SetActive(_isOn);
    }
    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
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
