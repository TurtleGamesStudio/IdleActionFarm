using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class RectTransformMover : MonoBehaviour
{
    private RectTransform _rectTransform;

    public void Init()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Move(Vector2 endValue, float duration)
    {
        _rectTransform.DOAnchorPos(endValue, duration);
    }
}
