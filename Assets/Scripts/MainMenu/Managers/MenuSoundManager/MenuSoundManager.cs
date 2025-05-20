using UnityEngine;

public class MenuSoundManager : MonoBehaviour, IMainMenuSound
{
    [SerializeField] private AudioSource _audioSource;

    public void PlayOneShot(AudioClip audioClip, float volumeScale = 1)
    {
        if (audioClip == null) return;

        _audioSource.PlayOneShot(audioClip, volumeScale);
    }
}
