using UnityEngine;

[RequireComponent(typeof(Animator))]
public class WeaponAnimator : MonoBehaviour
{
    private int Cut = Animator.StringToHash("Cut");

    [SerializeField] private UnpackedResourcesChecker _unpackedResourcesChecker;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _unpackedResourcesChecker.Cleared += OnZoneCleared;
        _unpackedResourcesChecker.TargetDetected += OnTargetDetected;
    }

    private void OnDisable()
    {
        _unpackedResourcesChecker.Cleared -= OnZoneCleared;
        _unpackedResourcesChecker.TargetDetected -= OnTargetDetected;
    }

    private void OnZoneCleared()
    {
        _animator.SetBool(Cut, false);
    }

    private void OnTargetDetected()
    {
        _animator.SetBool(Cut, true);
    }
}
