using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bag : MonoBehaviour
{
    [SerializeField] private float _cooldown = 1;
    [SerializeField] private int _capacity = 1;

    private List<Item> _items;
    private bool _isAdditionAvailable;

    public int Capacity => _capacity;
    public IReadOnlyList<Item> Items => _items;

    private void Awake()
    {
        _items = new List<Item>();
        _isAdditionAvailable = true;
    }

    public bool TryAdd(Item item)
    {
        if (CanAdd())
        {
            Add(item);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CanAdd()
    {
        return _items.Count < _capacity && _isAdditionAvailable;//_isAdditionAvailable - это про cooldown        
    }

    public void Add(Item item)
    {
        _items.Add(item);
        _isAdditionAvailable = false;
        Invoke(nameof(SetAdditionAvailable), _cooldown);
    }

    private void SetAdditionAvailable()
    {
        _isAdditionAvailable = true;
    }
}
