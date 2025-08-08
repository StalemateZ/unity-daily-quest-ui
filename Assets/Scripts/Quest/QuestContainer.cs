using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestContainer : MonoBehaviour
{
    public static event System.Action<Quest> OnQuestDetailsOpened;

    [Header("Unity References")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI title;
    private Quest questData;

    public void Setup(Quest questData)
    {
        this.questData = questData;
        icon.sprite = questData.Icon;
        title.text = questData.Title;
    }

    public void OpenDetails()
    {
        OnQuestDetailsOpened?.Invoke(questData);
    }
}
