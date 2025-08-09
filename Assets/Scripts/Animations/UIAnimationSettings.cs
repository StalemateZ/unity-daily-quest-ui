using System.Collections.Generic;
using UnityEngine;

public class UIAnimationSettings : MonoBehaviour
{
    public static UIAnimationSettings Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    #region Quest Detail Button
    [Header("Quest Detail Button")]
    [SerializeField] [Min(0f)] private float questDetailButtonScaleSize = 1.25f;

    public float QuestDetailButtonScaleSize
    {
        get { return Mathf.Clamp(questDetailButtonScaleSize, 0f, float.MaxValue); }
        set { questDetailButtonScaleSize = value; }
    }

    [SerializeField] [Min(0f)] private float questDetailButtonScaleDuration = 0.25f;

    public float QuestDetailButtonScaleDuration
    {
        get { return Mathf.Clamp(questDetailButtonScaleDuration, 0f, float.MaxValue); }
        set { questDetailButtonScaleDuration = value; }
    }
    #endregion

    #region Quest Pop Up Menu
    [Header("Quest Pop Up Menu")]
    [SerializeField][Min(0f)] private float questPopUpMenuScaleSize = 1.25f;

    public float QuestPopUpMenuScaleSize
    {
        get { return Mathf.Clamp(questPopUpMenuScaleSize, 0f, float.MaxValue); }
        set { questPopUpMenuScaleSize = value; }
    }

    [SerializeField][Min(0f)] private float questPopUpMenuScaleDuration = 0.25f;

    public float QuestPopUpMenuScaleDuration
    {
        get { return Mathf.Clamp(questPopUpMenuScaleDuration, 0f, float.MaxValue); }
        set { questPopUpMenuScaleDuration = value; }
    }
    #endregion
    
    #region Quest List
    [Header("Quest List")]
    [SerializeField][Min(0f)] private float questListIntroDuration = 0.25f;

    public float QuestListIntroDuration
    {
        get { return Mathf.Clamp(questListIntroDuration, 0f, float.MaxValue); }
        set { questListIntroDuration = value; }
    }

    [SerializeField][Min(0f)] private float questListOffsetMultiplier = 0.25f;

    public float QuestListOffsetMultiplier
    {
        get { return Mathf.Clamp(questListOffsetMultiplier, 0f, float.MaxValue); }
        set { questListOffsetMultiplier = value; }
    }
    #endregion
}
