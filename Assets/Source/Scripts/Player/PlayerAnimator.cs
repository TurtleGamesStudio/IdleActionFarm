using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private int Speed = Animator.StringToHash("Speed");
    private int Cut = Animator.StringToHash("Cut");

    [SerializeField] private UnpackedResourcesChecker _unpackedResourcesChecker;
    [SerializeField] private PlayerMovement _playerMovement;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _playerMovement.InputerShareChanged += OnInputerShareChanged;
        _unpackedResourcesChecker.Cleared += OnZoneCleared;
        _unpackedResourcesChecker.TargetDetected += OnTargetDetected;
    }

    private void OnDisable()
    {
        _playerMovement.InputerShareChanged -= OnInputerShareChanged;
        _unpackedResourcesChecker.Cleared -= OnZoneCleared;
        _unpackedResourcesChecker.TargetDetected -= OnTargetDetected;
    }

    private void OnInputerShareChanged(float value)
    {
        _animator.SetFloat(Speed, value);
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
