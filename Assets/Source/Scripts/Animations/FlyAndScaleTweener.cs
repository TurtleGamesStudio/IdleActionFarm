using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAndScaleTweener : MonoBehaviour
{
    //private TweenerCore<Vector3, Vector3, VectorOptions> _tweenerCore;
    private Sequence _sequence;

    public void Fly(float jumpPower, float time, Vector3 originalScale, float scaleMultiplier)
    {
        //_tweenerCore.Kill();
        //_tweenerCore = transform.DOLocalMove(Vector3.zero, time);
        //Sequence sequence = DOTween.Sequence();
        _sequence.Kill();
        _sequence = DOTween.Sequence();
        _sequence.Append(transform.DOLocalJump(Vector3.zero, jumpPower, 1, time));
        _sequence.Insert(0, transform.DOScale(scaleMultiplier, time / 2).OnComplete(() => ScaleToOrigin(originalScale, time / 2)));
        _sequence.Insert(0, transform.DOLocalRotateQuaternion(Quaternion.identity, time));
    }

    private void ScaleToOrigin(Vector3 originalScale, float time)
    {
        transform.DOScale(originalScale, time);
    }

    public void StopFlying()//желательно вернуть изначальный скейл)))
    {
        _sequence.Kill();
    }
}
