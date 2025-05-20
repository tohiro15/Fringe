using Unity.VisualScripting;
using UnityEngine;

public class MenuSoundManager : MonoBehaviour, IMainMenuSound
{
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip _buttonClickSound;
    [SerializeField] private AudioClip _buttonSelectionSound;

    public void PlayOneShot(AudioClip audioClip, float volumeScale = 1)
    {
        if (audioClip == null) return;

        _audioSource.PlayOneShot(audioClip, volumeScale);
    }

    public void PlayButtonClickSound()
    {
        if (_buttonClickSound == null) return;

        PlayOneShot(_buttonClickSound);
    }

    public void PlayButtonSelectionSound()
    {
        if (_buttonSelectionSound == null) return;

        PlayOneShot(_buttonSelectionSound);
    }
}
