using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class TweenerMover : MonoBehaviour
{
    private TweenerCore<Vector3, Vector3, VectorOptions> _tweenerCore;

    public void Move(Vector3 target ,float requireTime)
    {
        _tweenerCore.Kill();
        _tweenerCore = transform.DOMove(target, requireTime);
    }

    public void StopMoving()
    {
        _tweenerCore.Kill();
    }
}
