using UnityEngine.SceneManagement;
using UnityEngine;

public class GameFlowController : MonoBehaviour, IGameFlow
{
    public static ChapterData SelectedChapter;
    public static string TargetSceneName;


    public void SetChapter(ChapterData chapter)
    {
        SelectedChapter = chapter;
    }

    public ChapterData GetChapter()
    {
        return SelectedChapter;
    }

    public void StartGame()
    {
        if (SelectedChapter != null)
        {
            TargetSceneName = SelectedChapter.sceneName;
            SceneManager.LoadScene("LoadingScene");
        }
        else
        {
            Debug.LogError("Не выбрана глава!");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
