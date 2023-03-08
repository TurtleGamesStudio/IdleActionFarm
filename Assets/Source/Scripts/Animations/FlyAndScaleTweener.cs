using DG.Tweening;
using UnityEngine;
using System;

public class FlyAndScaleTweener : MonoBehaviour
{
    private Sequence _sequence;

    public event Action Completed;

    public void Fly(float jumpPower, float time, Vector3 originalScale, float scaleMultiplier)
    {
        _sequence.Kill();
        _sequence = DOTween.Sequence();
        _sequence.Append(transform.DOLocalJump(Vector3.zero, jumpPower, 1, time).OnComplete(() => Completed?.Invoke()));
        _sequence.Insert(0, transform.DOScale(scaleMultiplier, time / 2).OnComplete(() => ScaleToOrigin(originalScale, time / 2)));
        _sequence.Insert(0, transform.DOLocalRotateQuaternion(Quaternion.identity, time));
    }

    private void ScaleToOrigin(Vector3 originalScale, float time)
    {
        transform.DOScale(originalScale, time);
    }

    public void StopFlying()//желательно вернуть изначальный скейл, понадобится несколько анимационных скриптов, а не единая sequence)))
    {
        _sequence.Kill();
    }
}
