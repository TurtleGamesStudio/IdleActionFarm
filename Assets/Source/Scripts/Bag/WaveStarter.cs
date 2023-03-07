using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveStarter : MonoBehaviour
{
    [SerializeField] private float _delay;

    private WaitForSeconds _waiting;

    private void Awake()
    {
        _waiting = new WaitForSeconds(_delay);
    }

    public void StartWave(IReadOnlyList<Ring> rings)
    {
        StartCoroutine(Starting(rings));
    }

    public void Stop(IReadOnlyList<Ring> rings)
    {
        StartCoroutine(Stopping(rings));
    }

    private IEnumerator Starting(IReadOnlyList<Ring> rings)
    {
        for (int i = 0; i < rings.Count; i++)
        {
            rings[i].Move();
            yield return _waiting;
        }
    }

    private IEnumerator Stopping(IReadOnlyList<Ring> rings)
    {
        for (int i = 0; i < rings.Count; i++)
        {
            rings[i].StopMoving();
            yield return _waiting;
        }
    }
}
