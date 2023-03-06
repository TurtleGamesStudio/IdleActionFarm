using UnityEngine;
using System;

[RequireComponent(typeof(Collider))]
public class UnpackedResource : MonoBehaviour
{
    private int Drop = Animator.StringToHash("Drop");

    [SerializeField] private GameObject _wholeModel;
    [SerializeField] private GameObject _root;
    [SerializeField] private ResourceProducer _resourceProducer;
    [SerializeField] private Animator _animator;

    [SerializeField] private float _respawnDelay = 10;

    private Collider _collider;

    public event Action Packed;
    public event Action Growed;

    public bool IsAvalable;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        IsAvalable = true;
    }

    private void OnEnable()
    {
        _resourceProducer.Produced += OnProduced;
    }

    private void OnDisable()
    {
        _resourceProducer.Produced -= OnProduced;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Blade>())
        {
            Cut();
        }
    }

    private void Cut()
    {
        IsAvalable = false;
        Packed?.Invoke();
        _collider.enabled = false;
        _wholeModel.SetActive(false);
        _root.SetActive(true);
        _animator.SetTrigger(Drop);
    }

    private void Grow()
    {
        IsAvalable = true;
        Growed?.Invoke();
        _collider.enabled = true;
        _wholeModel.SetActive(true);
        _root.SetActive(false);
    }

    private void OnProduced()
    {
        Invoke(nameof(Grow), _respawnDelay);
    }
}
