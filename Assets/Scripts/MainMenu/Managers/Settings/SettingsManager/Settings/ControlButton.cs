using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ControlButton : MonoBehaviour
{
    [SerializeField] private string _actionMapName = "Player";
    [SerializeField] private string _actionName;

    [SerializeField] private Button _rebindButton;
    [SerializeField] private TMP_Text _rebindLabel;

    private InputAction _inputAction;

    private void Start()
    {
        if (!string.IsNullOrEmpty(_actionName))
        {
            _inputAction = SettingsManager.Instance.GetAction(_actionMapName, _actionName);
        }

        if (_rebindButton == null)
            _rebindButton = GetComponent<Button>();

        UpdateRebindLabel();
    }

    private void OnEnable()
    {
        _rebindButton.onClick.AddListener(OnRebindButtonClicked);
    }

    private void OnDisable()
    {
        _rebindButton.onClick.RemoveListener(OnRebindButtonClicked);
    }

    private void OnRebindButtonClicked()
    {
        SettingsManager.Instance.Rebind(_rebindButton, _rebindLabel, _inputAction, 0);
    }

    private void UpdateRebindLabel()
    {
        if (_inputAction != null && _inputAction.bindings.Count > 0)
        {
            string path = _inputAction.bindings[0].effectivePath;
            string readableName = InputControlPath.ToHumanReadableString(path,
                InputControlPath.HumanReadableStringOptions.OmitDevice);
            _rebindLabel.text = readableName;
        }
    }
}
