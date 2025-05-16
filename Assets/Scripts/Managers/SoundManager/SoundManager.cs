using UnityEngine;

public class SoundManager : MonoBehaviour, ISound
{
    [SerializeField] private AudioSource _flashlightAudioSource;
    [SerializeField] private AudioClip _flashlightToggleSound;

    public void PlayOneShotSound(AudioClip clip, AudioSource source)
    {
        if (clip != null && source != null)
            source.PlayOneShot(clip);
    }
    public void PlayFlashlightToggleSound()
    { 
        PlayOneShotSound(_flashlightToggleSound, _flashlightAudioSource);
    }
}
