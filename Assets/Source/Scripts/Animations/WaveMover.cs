using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMover : MonoBehaviour
{
    [SerializeField] private float _deflection = 1;
    [SerializeField] private float _duration = 1;

    private float _quaterDuration;
    private float _halfDuration;

    private Sequence _sequence;

    private void Awake()
    {
        _quaterDuration = _duration / 4;
        _halfDuration = _duration / 2;
    }

    public void Move()
    {
        StartFromCurrentPoint();

        _sequence.OnComplete(StartFromEndPoint);
    }

    private void StartFromEndPoint()
    {
        StartFromCurrentPoint();
        _sequence.SetLoops(-1);
    }

    private void StartFromCurrentPoint()
    {
        _sequence.Kill();
        _sequence = DOTween.Sequence();
        _sequence.Append(transform.DOLocalMoveX(_deflection, _quaterDuration).SetEase(Ease.OutSine));
        _sequence.Append(transform.DOLocalMoveX(-_deflection, _halfDuration).SetEase(Ease.InOutSine));
        _sequence.Append(transform.DOLocalMoveX(0, _quaterDuration).SetEase(Ease.InSine));
    }

    public void StopMoving()
    {
        _sequence.Kill();
    }
}
