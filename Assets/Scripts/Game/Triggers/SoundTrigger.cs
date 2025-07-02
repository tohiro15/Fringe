using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private bool _canRepeat;
    private bool _isPlaying = true;

    private void Awake()
    {
        if (_audioSource == null)
            Debug.LogWarning("AudioSource is not assigned in SoundTrigger on " + gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || !_isPlaying)
            return;

        _audioSource.PlayOneShot(_audioSource.clip);

        if (!_canRepeat)
            _isPlaying = false;
    }


}
