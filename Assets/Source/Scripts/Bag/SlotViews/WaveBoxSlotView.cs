using System.Collections.Generic;
using UnityEngine;

public class WaveBoxSlotView : MonoBehaviour, ISlotView
{
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private Transform _container;
    [SerializeField] private Ring _template;
    [SerializeField] private WaveFloorStarter _starter;
    [SerializeField] private Vector3 _offset = Vector3.one;
    [SerializeField] private Vector2Int _baseSize = Vector2Int.one;

    private List<Ring> _rings;
    private List<Slot> _slots;
    private List<List<Ring>> _floors;

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
        _slots = new List<Slot>();
        _floors = new List<List<Ring>>();

        for (int i = 0; i < capacity; i++)
        {
            Vector3 spawnPosition = GetPosition(i);
            Ring ring = Instantiate(_template, spawnPosition, Quaternion.identity, transform);
            ring.Init();
            _rings.Add(ring);
            Slot slot = ring.GetComponent<Slot>();
            _slots.Add(slot);
        }

        _floors = ListConverter.GetSeparatedList(_rings, _baseSize.x * _baseSize.y);
    }

    public Slot GetFreeSlot()
    {
        foreach (Slot slot in _slots)
        {
            if (slot.Item == null)
            {
                return slot;
            }
        }

        return null;
    }

    public Slot FindSlot(Item item)
    {
        foreach (Slot slot in _slots)
        {
            if (slot.Item == item)
            {
                return slot;
            }
        }

        return null;
    }

    private Vector3 GetPosition(int index)
    {
        int column = index % _baseSize.x;
        int row = index / _baseSize.x % _baseSize.y;
        int floor = index / (_baseSize.x * _baseSize.y);

        Vector3 position = new Vector3(column * _offset.x, floor * _offset.y, row * _offset.z);

        return _container.position + position;
    }

    private void OnMoved()
    {
        _starter.StartWave(_floors);
    }

    private void OnStopped()
    {
        _starter.Stop(_floors);
    }
}
