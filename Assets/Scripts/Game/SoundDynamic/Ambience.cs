using UnityEngine;

public class Ambience : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private AudioSource _AmbientAudioSource;
    [SerializeField] private Collider _collider;

    private void Start()
    {
        if (_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    private void Update()
    {
        if (_player != null && _AmbientAudioSource != null && _collider != null)
        {
            Vector3 closestPoint = _collider.ClosestPoint(_player.transform.position);

            _AmbientAudioSource.transform.position = closestPoint;
        }
    }
}
