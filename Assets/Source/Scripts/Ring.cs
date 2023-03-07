using UnityEngine;

[RequireComponent(typeof(WaveMover))]
[RequireComponent(typeof(LocalMoverX))]
public class Ring : MonoBehaviour
{
    private WaveMover _waveMover;
    private LocalMoverX _localMover;

    public void Init()
    {
        _waveMover = GetComponent<WaveMover>();
        _localMover = GetComponent<LocalMoverX>();
    }

    public void Move()
    {
        _localMover.StopMoving();
        _waveMover.Move();
    }

    public void StopMoving()
    {
        _waveMover.StopMoving();
        _localMover.Move(0);
    }
}
