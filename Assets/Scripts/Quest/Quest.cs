using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Quest_", menuName = "Quest")]
public class Quest : ScriptableObject
{
    [SerializeField] private Sprite icon;

    public Sprite Icon
    {
        get { return icon; }
        set { icon = value; }
    }

    [SerializeField] private string title;

    public string Title
    {
        get { return title; }
        set { title = value; }
    }

    [SerializeField] [TextArea] private string description;

    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    [SerializeField] private QuestStatus status;

    public QuestStatus Status
    {
        get { return status; }
        set { status = value; }
    }
}
