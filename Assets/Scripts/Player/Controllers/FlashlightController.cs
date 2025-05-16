using System;
using UnityEngine;

public class FlashlightController : MonoBehaviour, IFlashlight
{
    private bool _isOn = false;
    public event Action OnFlashlightToggled;


    public void ToggleFlashlight()
    {
        _isOn = !_isOn;
        gameObject.SetActive(_isOn);
        GameManager.Instance.Sound?.PlayFlashlightToggleSound();
        OnFlashlightToggled?.Invoke();
    }
}
