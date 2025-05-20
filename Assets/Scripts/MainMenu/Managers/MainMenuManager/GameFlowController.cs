using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowController : MonoBehaviour,IGameFlow
{
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
