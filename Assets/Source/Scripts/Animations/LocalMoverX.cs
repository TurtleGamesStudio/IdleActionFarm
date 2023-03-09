using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using DG.Tweening;
using UnityEngine;

public class LocalMoverX : MonoBehaviour
{
    [SerializeField] private float _duration = 1;

    private TweenerCore<Vector3, Vector3, VectorOptions> _tweenerCore;

    public void Move(float endValue)
    {
        _tweenerCore.Kill();
        _tweenerCore = transform.DOLocalMoveX(endValue, _duration);
    }

    public void StopMoving()
    {
        _tweenerCore.Kill();
    }
}
