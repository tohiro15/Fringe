using UnityEngine;

public class SoundManager : MonoBehaviour, ISound
{
    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    [SerializeField] private AudioSource _flashlightAudioSource;

    [Header("Player")]
    [Space]
    [Header("Flashlight")]

    [SerializeField] private AudioClip _flashlightToggleSound;

    [Header("Map")]
    [Space]
    [Header("Door")]

    [SerializeField] private AudioClip _singleDoorOpenSound;
    [SerializeField] private AudioClip _singleDoorCloseSound;

    [Space]

    [SerializeField] private AudioClip _doubleDoorOpenSound;
    [SerializeField] private AudioClip _doubleDoorCloseSound;

    [Space]

    [SerializeField] private AudioClip _slidingDoorOpenSound;
    [SerializeField] private AudioClip _slidingDoorCloseSound;
    public void PlayOneShotSound(AudioClip clip, AudioSource source)
    {
        if (clip != null && source != null)
            source.PlayOneShot(clip);
    }
    public void PlayFlashlightToggleSound()
    { 
        PlayOneShotSound(_flashlightToggleSound, _flashlightAudioSource);
    }

    public void PlayDoorSound(DoorType door, bool open, AudioSource source)
    {
        if (door == DoorType.SingleHindedDoor && open) PlayOneShotSound(_singleDoorOpenSound, source);
        else if (door == DoorType.SingleHindedDoor && !open) PlayOneShotSound(_singleDoorCloseSound, source);

        if (door == DoorType.DoubleDoor && open) PlayOneShotSound(_doubleDoorOpenSound, source);
        else if (door == DoorType.DoubleDoor && !open) PlayOneShotSound(_doubleDoorCloseSound, source);

        if (door == DoorType.SlidingDoor && open) PlayOneShotSound(_slidingDoorOpenSound, source);
        else if (door == DoorType.SlidingDoor && !open) PlayOneShotSound(_slidingDoorCloseSound, source);
    }
}

