using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestContainer : MonoBehaviour
{
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
}
