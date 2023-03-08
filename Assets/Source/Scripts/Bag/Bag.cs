using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bag : MonoBehaviour
{
    [SerializeField] private float _cooldown = 1;
    [SerializeField] private float _removeCooldown = 1;
    [SerializeField] private int _capacity = 1;

    private List<Item> _items;
    private bool _isAdditionAvailable;
    private bool _isRemoveAvailable;

    public int Capacity => _capacity;
    public IReadOnlyList<Item> Items => _items;
    public bool CanAdd => _items.Count < _capacity && _isAdditionAvailable;
    public bool CanRemove => _items.Count > 0 && _isRemoveAvailable;

    public event Action<Item> Added;

    private void Awake()
    {
        _items = new List<Item>();
        _isAdditionAvailable = true;
        _isRemoveAvailable = true;
    }

    public void Add(Item item)
    {
        _items.Add(item);
        _isAdditionAvailable = false;
        Invoke(nameof(SetAdditionAvailable), _cooldown);
        Added?.Invoke(item);
    }

    public Item GetAt(int index)
    {
        return _items[index];
    }

    public void Remove(Item item)
    {
        _items.Remove(item);
        _isRemoveAvailable = false;
        Invoke(nameof(SetRemoveAvailable), _removeCooldown);

    }

    private void SetAdditionAvailable()
    {
        _isAdditionAvailable = true;
    }

    private void SetRemoveAvailable()
    {
        _isRemoveAvailable = true;
    }
}
