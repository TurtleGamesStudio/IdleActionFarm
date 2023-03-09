using System.Collections.Generic;
using UnityEngine;

public class BoxSlotView : MonoBehaviour, ISlotView
{
    [SerializeField] private Vector3 _offset = Vector3.one;
    [SerializeField] private Transform _container;
    [SerializeField] private Slot _template;
    [SerializeField] private Vector2Int _baseSize = Vector2Int.one;

    private List<Slot> _slots;

    public void Init(int capacity)
    {
        _slots = new List<Slot>();

        for (int i = 0; i < capacity; i++)
        {
            Vector3 spawnPosition = GetPosition(i);
            Slot slot = Instantiate(_template, spawnPosition, Quaternion.identity, _container.transform);
            _slots.Add(slot);
        }
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
}
