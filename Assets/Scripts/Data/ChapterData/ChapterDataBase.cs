using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChapterDatabase", menuName = "Scripts/MainMenu/Data/ChapterDataBase")]
public class ChapterDatabase : ScriptableObject
{
    public List<ChapterData> Chapters;
}
