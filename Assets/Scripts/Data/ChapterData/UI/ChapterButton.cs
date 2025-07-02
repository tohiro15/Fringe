using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChapterButton : MonoBehaviour
{
    public ChapterData ChapterData;
    public TMP_Text Label;
    [SerializeField] private Button _button;

    public void Init(System.Action<ChapterData> onClickCallback)
    {
        if (Label != null && ChapterData != null)
        {
            Label.text = $"√À¿¬¿ \"{ChapterData.chapterName}\"";
        }

        _button?.onClick.RemoveAllListeners();
        _button?.onClick.AddListener(() => onClickCallback?.Invoke(ChapterData));
    }

    public void Highlight(bool isSelected)
    {
        if (Label != null)
        {
            Label.text = isSelected ? $"<u>√À¿¬¿ \"{ChapterData.chapterName}\"</u>" : $"√À¿¬¿ \"{ChapterData.chapterName}\"";
        }
    }
}
