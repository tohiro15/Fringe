using UnityEngine;
using UnityEngine.InputSystem;

public interface IMovementStrategy
{
    float Speed { get; }
    void Move(Transform transform);
    void HandleAnimation(Vector3 inputDirection);
}

public interface IRotatable
{
    Camera Camera { get; }
    void Init(InputAction lookAction, float sensitivity);
    void HandleInput();
    void Rotate(Rigidbody rb);
}

public interface IJump
{
    void Init(InputAction jumpAction, float force, Transform groundCheck, float groundDistance);
    void HandleInput(Rigidbody rb);
    void CheckGround();
}

public interface ICrouch
{
    void Init(InputAction crouchAction, CapsuleCollider playerCollider, Transform headChecker, float headCheckRadius, Vector3 crouchCameraOffset, IAnimation anim);
    void HandleInput();
    bool IsCrouching { get; }
}

public interface IFlashlight
{
    void Init(InputAction toggleAction, GameObject flashlightGO);
    void HandleInput();
}

public interface IAnimation
{
    void PlayIdle();
    void PlayWalk();
    void PlayRun();
    void PlayCrouchStart();
    void PlayCrouchEnd();
}

public interface IDoorController
{
    void Init(InputAction interactAction, Camera playerCamera, float interactDistance, System.Action<bool> showPrompt);
    void HandleInteraction();
}

public interface ISettings
{
    InputActionAsset InputActions { get; }
    float Sensitivity { get; }
    void ChangeQuality(int index);
    int GetQualityLevel();
    void ChangeResolution(int index);
    int GetResolution();
    void GetCurrentResolution();
    void ChangeWindowMode();
    bool GetIsWindowMode();
    void ChangeSFXVolume(float value);
    void ChangeMusicVolume(float value);
    float GetSFXVolume();
    float GetMusicVolume();
    void ChangeSensitivity(float newSensitivity);
    InputAction GetAction(string mapName, string actionName);
    InputActionAsset GetInputActionsAsset();
    void LoadSettings();
    void SaveSettings();
}

public interface IUI
{
    void Init();
    void UpdateInteractionImage(bool isActive);
    void OpenPauseMenu();
    void ClosePauseMenu();
    void OpenSettingsMenu();
    void SetCanvas(bool game, bool pause, bool settings);
}
