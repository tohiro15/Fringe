using UnityEngine;
using UnityEngine.Playables;

public class TimelineStarter : MonoBehaviour
{
    [SerializeField] private PlayableDirector _playableDirector;
    private void Start()
    {
        _playableDirector = GetComponent<PlayableDirector>();
    }
    private void Update()
    {
        InputHandler();
    }

    public void InputHandler()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartDirector();
        }
    }

    public void StartDirector()
    {
        _playableDirector?.Play();

        if(Cursor.visible)
        {
            Cursor.visible = false;
        }
    }
}
