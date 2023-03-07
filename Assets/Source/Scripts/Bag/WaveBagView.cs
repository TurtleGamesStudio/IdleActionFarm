using System.Collections.Generic;
using UnityEngine;

public class WaveBagView : MonoBehaviour, IBagView
{
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private Transform _container;
    [SerializeField] private Ring _template;
    [SerializeField] private WaveStarter _starter;

    private List<Ring> _rings;
    private List<Transform> _places;

    public IReadOnlyList<Transform> Places => _places;

    private void OnEnable()
    {
        _movement.Moved += OnMoved;
        _movement.Stopped += OnStopped;
    }

    private void OnDisable()
    {
        _movement.Moved -= OnMoved;
        _movement.Stopped -= OnStopped;
    }

    public void Init(int capacity)
    {
        _rings = new List<Ring>();
        _places = new List<Transform>();

        for (int i = 0; i < capacity; i++)
        {
            Vector3 spawnPosition = GetPosition(i);
            Ring ring = Instantiate(_template, spawnPosition, Quaternion.identity, transform);
            ring.Init(transform);
            _rings.Add(ring);
            _places.Add(ring.transform);
        }
    }

    //Increase capacity unavailable
    private Vector3 GetPosition(int index)
    {
        return _container.position + Vector3.up * index;
    }

    private void OnMoved()
    {
        _starter.StartWave(_rings);
    }

    private void OnStopped()
    {
        _starter.Stop(_rings);
    }
}
