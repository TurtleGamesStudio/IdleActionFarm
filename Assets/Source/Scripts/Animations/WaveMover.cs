using DG.Tweening;
using UnityEngine;

public class WaveMover : MonoBehaviour
{
    [SerializeField] private float _deflection = 1;
    [SerializeField] private float _duration = 1;

    private float _startPositionX;
    private float _quaterDuration;
    private float _halfDuration;

    private Sequence _sequence;

    public void Init()
    {
        _startPositionX = transform.position.x;
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

    public void StopMoving()
    {
        _sequence.Kill();
    }

    private void StartFromCurrentPoint()
    {
        _sequence.Kill();
        _sequence = DOTween.Sequence();
        _sequence.Append(transform.DOLocalMoveX(_startPositionX + _deflection, _quaterDuration).SetEase(Ease.OutSine));
        _sequence.Append(transform.DOLocalMoveX(_startPositionX - _deflection, _halfDuration).SetEase(Ease.InOutSine));
        _sequence.Append(transform.DOLocalMoveX(_startPositionX, _quaterDuration).SetEase(Ease.InSine));
    }
}
