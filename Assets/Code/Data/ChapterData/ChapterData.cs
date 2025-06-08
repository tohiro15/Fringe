using UnityEngine;

[CreateAssetMenu(fileName = "ChapterData", menuName = "Scripts/MainMenu/Data/ChapterData")]
public class ChapterData : ScriptableObject
{
    public string chapterName;
    public string sceneName;
    public Sprite previewImage;
    [TextArea] public string description;
}
