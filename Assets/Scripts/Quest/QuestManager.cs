using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private Quest[] quests;

    [Header("Unity Reference")]
    [SerializeField] private Transform questListParent;
    [SerializeField] private GameObject questCardPrefab;

    private void Awake()
    {
        SetupQuestList();
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
}
