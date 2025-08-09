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
    [SerializeField] private GameObject darkenEffect;
    [SerializeField] private GameObject canClaimIcon;
    [SerializeField] private GameObject outline;
    private Quest questData;

    public void Setup(Quest questData)
    {
        this.questData = questData;
        icon.sprite = questData.Icon;
        title.text = questData.Title;

        darkenEffect.SetActive(questData.Status == QuestStatus.Claimed);
        canClaimIcon.SetActive(questData.Status == QuestStatus.Unclaimed);
        outline.SetActive(questData.Status == QuestStatus.Unclaimed);
    }

    public void OpenDetails()
    {
        OnQuestDetailsOpened?.Invoke(questData);
    }
}
