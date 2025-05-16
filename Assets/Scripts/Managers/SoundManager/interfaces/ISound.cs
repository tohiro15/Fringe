using UnityEngine;

public interface ISound
{
    void PlayOneShotSound(AudioClip clip, AudioSource source);
    void PlayFlashlightToggleSound();
}
