using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowController : MonoBehaviour, IGameFlow
{
    private int _selectedChapterIndex = -1;

    public void SetChapterIndex(int index)
    {
        _selectedChapterIndex = index;
    }
    public int GetChapterIndex()
    {
        return _selectedChapterIndex;
    }
    public void StartGame()
    {
        if (_selectedChapterIndex > -1)
        {
            SceneManager.LoadScene(_selectedChapterIndex);
        }
        else
        {
            Debug.LogError("Такой сцены не существует!");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
