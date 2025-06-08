using UnityEngine;

public abstract class DoorBase : MonoBehaviour, IDoor
{
    protected bool _isOpen;
    [SerializeField] protected AudioSource _audioSource;
    [SerializeField] protected DoorType _type;

    public abstract void Interaction();
}
