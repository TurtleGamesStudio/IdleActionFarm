using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveFloorStarter : MonoBehaviour
{
    [SerializeField] private float _delay;

    private WaitForSeconds _waiting;

    private void Awake()
    {
        _waiting = new WaitForSeconds(_delay);
    }

    public void StartWave(IReadOnlyList<IReadOnlyList<Ring>> floors)
    {
        StartCoroutine(Starting(floors));
    }

    public void Stop(IReadOnlyList<IReadOnlyList<Ring>> floors)
    {
        StartCoroutine(Stopping(floors));
    }

    private IEnumerator Starting(IReadOnlyList<IReadOnlyList<Ring>> floors)
    {
        foreach (var floor in floors)
        {
            MoveFloor(floor);
            yield return _waiting;
        }
    }

    private IEnumerator Stopping(IReadOnlyList<IReadOnlyList<Ring>> floors)
    {
        foreach (var floor in floors)
        {
            StopMoveFloor(floor);
            yield return _waiting;
        }
    }

    private void MoveFloor(IReadOnlyList<Ring> rings)
    {
        foreach (Ring ring in rings)
        {
            ring.Move();
        }
    }

    private void StopMoveFloor(IReadOnlyList<Ring> rings)
    {
        foreach (Ring ring in rings)
        {
            ring.StopMoving();
        }
    }
}
