using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestDetailsButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Vector3 originalSize;
    [SerializeField] private Button button;

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
}
