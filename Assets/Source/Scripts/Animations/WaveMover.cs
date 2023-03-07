using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMover : MonoBehaviour
{
    [SerializeField] private float _deflection;
    [SerializeField] private float _duration;

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
        _sequence.Kill();
        _sequence = DOTween.Sequence();
        _sequence.Append(transform.DOLocalMoveX(_deflection, _quaterDuration));
        _sequence.Append(transform.DOLocalMoveX(-_deflection, _halfDuration));
        _sequence.Append(transform.DOLocalMoveX(0, _quaterDuration));

        _sequence.SetLoops(-1);
    }

    public void StopMoving()
    {
        _sequence.Kill();
    }
}
