using UnityEngine;

public interface ISound
{
    void PlayOneShotSound(AudioClip clip, AudioSource source);
    void PlayFlashlightToggleSound();

    void PlayDoorSound(DoorType door, bool open, AudioSource source);
}
