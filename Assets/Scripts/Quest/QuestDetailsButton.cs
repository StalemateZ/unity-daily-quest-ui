using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestDetailsButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    private Vector3 originalSize;
    public Vector3 OriginalSize
    {
        get { return originalSize; }
        set { originalSize = value; }
    }

    [SerializeField] private Button button;
    public Button Button
    {
        get { return button; }
        set { button = value; }
    }

    private void Awake()
    {
        originalSize = transform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!button.interactable) return;
        transform.DOScale(UIAnimationSettings.Instance.QuestDetailButtonScaleSize, UIAnimationSettings.Instance.QuestDetailButtonScaleDuration).SetEase(Ease.OutBack);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!button.interactable) return;
        transform.DOScale(originalSize, UIAnimationSettings.Instance.QuestDetailButtonScaleDuration).SetEase(Ease.OutBack);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!button.interactable) return;
        transform.DOScale(originalSize, UIAnimationSettings.Instance.QuestDetailButtonScaleDuration).SetEase(Ease.OutBack);
    }
}
