using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{

    [SerializeField] private Slider _loadingBar;
    [SerializeField] private TMP_Text _chapterDescription;
    [SerializeField] private TMP_Text _loadingText;
    [SerializeField] private TMP_Text _startGameText;
    [SerializeField] private Image _chapterBackgroundImage;

    private InputAction _startGameInputAction;

    private void OnEnable()
    {
        SettingsManager.Instance.GetInputActionsAsset().FindActionMap("UI")?.Enable();
    }

    private void OnDisable()
    {
        SettingsManager.Instance.GetInputActionsAsset().FindActionMap("UI")?.Disable();
    }
    IEnumerator Start()
    {
        _startGameInputAction = SettingsManager.Instance.GetAction("UI", "StartGame");

        _loadingBar?.gameObject.SetActive(true);
        _loadingText?.gameObject.SetActive(true);
        _startGameText?.gameObject.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if(_chapterDescription != null) _chapterDescription.text = GameFlowController.SelectedChapter.description;
        if(_chapterBackgroundImage != null) _chapterBackgroundImage.sprite = GameFlowController.SelectedChapter.previewImage;

        AsyncOperation operation = SceneManager.LoadSceneAsync(GameFlowController.TargetSceneName);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            _loadingBar.value = progress;

            if (progress >= 1f)
            {
                _loadingBar?.gameObject.SetActive(false);
                _loadingText?.gameObject.SetActive(false);
                _startGameText?.gameObject.SetActive(true);

                string keyName = GetBindingNameForCurrentDevice(_startGameInputAction);

                _startGameText.text = $"Нажмите {keyName} чтобы начать историю...";

                if ((_startGameInputAction != null && _startGameInputAction.WasPressedThisFrame()) || Input.anyKeyDown)
                {
                    operation.allowSceneActivation = true;
                }
            }


            yield return null;
        }
    }

    private string GetBindingNameForCurrentDevice(InputAction action)
    {
        if (action == null)
        {
            Debug.LogWarning("GetBindingNameForCurrentDevice: action is null");
            return "клавишу";
        }

        if (action.bindings.Count == 0)
        {
            Debug.LogWarning("GetBindingNameForCurrentDevice: no bindings found");
            return "клавишу";
        }

        var device = InputSystem.devices.FirstOrDefault(d => d.enabled);
        if (device == null)
        {
            Debug.LogWarning("GetBindingNameForCurrentDevice: no enabled devices found");
            return action.bindings[0].ToDisplayString();
        }

        string deviceLayout = device.layout;
        if (string.IsNullOrEmpty(deviceLayout))
        {
            Debug.LogWarning("GetBindingNameForCurrentDevice: device layout is null or empty");
            return action.bindings[0].ToDisplayString();
        }

        foreach (var binding in action.bindings)
        {
            if (!string.IsNullOrEmpty(binding.effectivePath) && binding.effectivePath.StartsWith($"<{deviceLayout}>"))
            {
                return binding.ToDisplayString();
            }
        }

        return action.bindings[0].ToDisplayString();
    }

}
