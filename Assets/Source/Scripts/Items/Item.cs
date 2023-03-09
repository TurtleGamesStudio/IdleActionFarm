using UnityEngine;
using System;

public class Item : MonoBehaviour
{
    [SerializeField] private FlyAndScaleTweener _tweener;
    [SerializeField] private Vector3 _originalScale = Vector3.one;
    [SerializeField] private float _jumpPower = 1;
    [SerializeField] private float _time = 3;
    [SerializeField] private float _scaleMultiplier = 1.5f;

    private Collider _collider;

    public event Action<Item> Placed;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        _tweener.Completed += OnAnimationComplete;
    }

    private void OnDisable()
    {
        _tweener.Completed -= OnAnimationComplete;
    }

    public void Fly(Transform target)
    {
        transform.parent = target;
        _tweener.Fly(_jumpPower, _time, _originalScale, _scaleMultiplier);
    }

    private void OnAnimationComplete()
    {
        Placed?.Invoke(this);
    }

    public void DisableCollider()
    {
        _collider.enabled = false;
    }
}
