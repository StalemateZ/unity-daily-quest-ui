using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuestDetailsButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Vector3 originalSize;

    private void Awake()
    {
        originalSize = transform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.DOScale(UIAnimationSettings.Instance.QuestDetailButtonScaleSize, UIAnimationSettings.Instance.QuestDetailButtonScaleDuration).SetEase(Ease.OutBack);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.DOScale(originalSize, UIAnimationSettings.Instance.QuestDetailButtonScaleDuration).SetEase(Ease.OutBack);
    }
}
