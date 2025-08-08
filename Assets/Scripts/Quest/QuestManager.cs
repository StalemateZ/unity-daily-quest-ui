using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private Quest[] quests;
    private Quest activeQuest;

    [Header("Quest List")]
    [SerializeField] private Transform questListParent;
    [SerializeField] private GameObject questCardPrefab;
    [Header("Quest Details")]
    [SerializeField] private GameObject questDetailMenu;
    [SerializeField] private Transform questDetailPanel;
    private Vector3 questDetailPanelOriginalSize;
    [SerializeField] private TextMeshProUGUI questDetailTitle;
    [SerializeField] private TextMeshProUGUI questDetailDesc;
    [SerializeField] private Button questDetailButton;
    [SerializeField] private TextMeshProUGUI questDetailButtonText;
    [SerializeField] private Image questDetailButtonImage;
    [SerializeField] private Color questDetailUnclaimedColor;
    [SerializeField] private Color questDetailClaimedColor;
    private const string QUEST_IN_PROGRESS = "IN PROGRESS";
    private const string QUEST_CLAIM = "CLAIM";
    private const string QUEST_CLAIMED = "CLAIMED";
    [Header("Particles")]
    [SerializeField] private GameObject sparkleParticle;

    private void Awake()
    {
        questDetailPanelOriginalSize = questDetailPanel.localScale;
        SetupQuestList();
    }

    private void OnEnable()
    {
        QuestContainer.OnQuestDetailsOpened += OpenDetailsMenu;
    }

    private void OnDisable()
    {
        QuestContainer.OnQuestDetailsOpened -= OpenDetailsMenu;
    }

    private void SetupQuestList()
    {
        foreach (Quest quest in quests)
        {
            GameObject questCard = Instantiate(questCardPrefab, questListParent);
            QuestContainer container = questCard.GetComponent<QuestContainer>();
            container.Setup(quest);
        }
    }

    

    private void SetupQuestDetails(Quest data)
    {
        activeQuest = data;

        questDetailTitle.text = data.Title;
        questDetailDesc.text = data.Description;
        switch (data.Status)
        {
            case QuestStatus.InProgress:
                questDetailButtonText.text = QUEST_IN_PROGRESS;
                questDetailButtonImage.color = questDetailClaimedColor;
                questDetailButton.interactable = false;
                break;
            case QuestStatus.Unclaimed:
                questDetailButtonText.text = QUEST_CLAIM;
                questDetailButtonImage.color = questDetailUnclaimedColor;
                questDetailButton.interactable = true;
                break;
            case QuestStatus.Claimed:
                questDetailButtonText.text = QUEST_CLAIMED;
                questDetailButtonImage.color = questDetailClaimedColor;
                questDetailButton.interactable = false;
                break;
            default:
                break;
        }
    }

    private void OpenDetailsMenu(Quest data)
    {
        SetupQuestDetails(data);

        questDetailMenu.SetActive(true);
        questDetailPanel.DOScale(UIAnimationSettings.Instance.QuestPopUpMenuScaleSize, UIAnimationSettings.Instance.QuestPopUpMenuScaleDuration).SetEase(Ease.OutBack).From();
    }

    public void CloseDetailsMenu()
    {
        questDetailPanel.DOScale(UIAnimationSettings.Instance.QuestPopUpMenuScaleSize, UIAnimationSettings.Instance.QuestPopUpMenuScaleDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            questDetailMenu.SetActive(false);
            questDetailPanel.localScale = questDetailPanelOriginalSize;
        });
    }

    public void ClaimQuest()
    {
        activeQuest.Status = QuestStatus.Claimed;
        GameObject particle = Instantiate(sparkleParticle);
        particle.transform.position = questDetailButton.transform.position;
        SetupQuestDetails(activeQuest);
    }
}
