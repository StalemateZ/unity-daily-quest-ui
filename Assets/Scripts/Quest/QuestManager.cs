using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    private struct QuestGameObject
    {
        public Quest quest;
        public GameObject gameObject;
        public QuestContainer container;

        public QuestGameObject(Quest quest, GameObject gameObject, QuestContainer container)
        {
            this.quest = quest;
            this.gameObject = gameObject;
            this.container = container;
        }
    }

    [SerializeField] private Quest[] quests;
    private List<QuestGameObject> questGameObjects;
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
    [SerializeField] private QuestDetailsButton questDetailButton;
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
        questGameObjects = new List<QuestGameObject>();

        //This makes the last color of the gradient the claimed color.
        GradientColorKey[] colorKeys = UIAnimationSettings.Instance.QuestClaimButtonGradient.colorKeys;
        colorKeys[colorKeys.Length - 1].color = questDetailClaimedColor;
        UIAnimationSettings.Instance.QuestClaimButtonGradient.SetKeys(colorKeys, UIAnimationSettings.Instance.QuestClaimButtonGradient.alphaKeys);

        SortQuests();
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

    private void SortQuests()
    {
        quests = quests
            .OrderByDescending(q => q.Status == QuestStatus.Unclaimed)
            .ThenByDescending(q => q.Status == QuestStatus.InProgress)
            .ToArray();
    }

    private void SortGameObjects()
    {
        questGameObjects = questGameObjects
            .OrderByDescending(q => q.quest.Status == QuestStatus.Unclaimed)
            .ThenByDescending(q => q.quest.Status == QuestStatus.InProgress)
            .ToList();

        for (int i = 0; i < questGameObjects.Count; i++)
        {
            questGameObjects[i].gameObject.transform.SetSiblingIndex(i);
            questGameObjects[i].container.Setup(questGameObjects[i].quest);
        }
    }

    private void SetupQuestList()
    {
        questCardPrefab.SetActive(false);
        for (int i = 0; i < quests.Length; i++)
        {
            GameObject questCard = Instantiate(questCardPrefab, questListParent);

            QuestContainer container = questCard.GetComponentInChildren<QuestContainer>();
            questGameObjects.Add(new QuestGameObject(quests[i], questCard, container));
            
            container.Setup(quests[i]);
            
            RectTransform rectTransform = container.GetComponent<RectTransform>();

            Sequence questIntro = DOTween.Sequence();

            questIntro
                .Insert(
                UIAnimationSettings.Instance.QuestListOffsetMultiplier * i,
                rectTransform.DOAnchorPosX(
                    (rectTransform.anchoredPosition.x - rectTransform.sizeDelta.x),
                    UIAnimationSettings.Instance.QuestListIntroDuration
                ).From())
                .InsertCallback(
                UIAnimationSettings.Instance.QuestListOffsetMultiplier * i,
                () =>
                {
                    questCard.SetActive(true);
                }
                );
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
                questDetailButton.Button.interactable = false;
                break;
            case QuestStatus.Unclaimed:
                questDetailButtonText.text = QUEST_CLAIM;
                questDetailButtonImage.color = questDetailUnclaimedColor;
                questDetailButton.Button.interactable = true;
                break;
            case QuestStatus.Claimed:
                questDetailButtonText.text = QUEST_CLAIMED;
                questDetailButtonImage.color = questDetailClaimedColor;
                questDetailButton.Button.interactable = false;
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

        questDetailButtonImage.DOGradientColor(UIAnimationSettings.Instance.QuestClaimButtonGradient, UIAnimationSettings.Instance.QuestClaimDuration).SetEase(Ease.OutSine);
        questDetailButton.transform.localScale = Vector3.one * UIAnimationSettings.Instance.QuestClaimButtonScaleSize;
        questDetailButton.transform.DOScale(questDetailButton.OriginalSize, UIAnimationSettings.Instance.QuestClaimDuration).SetEase(Ease.OutSine);

        SortGameObjects();
        SetupQuestDetails(activeQuest);
    }
}
