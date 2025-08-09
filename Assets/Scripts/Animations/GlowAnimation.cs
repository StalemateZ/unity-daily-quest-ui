using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowAnimation : MonoBehaviour
{
    [SerializeField] private float glowAnimationScaleSize = 5f;

    [SerializeField] private float glowAnimationScaleDuration = 0.5f;

    private void Awake()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        DOTween.Sequence()
            .Insert(0, transform.DOScale(glowAnimationScaleSize, glowAnimationScaleDuration).SetEase(Ease.OutExpo))
            .Insert(0, sr.DOFade(0f, glowAnimationScaleDuration).SetEase(Ease.InSine));
    }
}
