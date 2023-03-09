using System.Collections.Generic;
using UnityEngine;
using System;

public class BagHolder : MonoBehaviour, IBagHolder
{
    [SerializeField] private Bag _bag;
    [SerializeField] private MonoBehaviour _slotView;

    public ISlotView _view => (ISlotView)_slotView;

    public IReadOnlyList<Item> Items => _bag.Items;
    public bool CanAdd => _bag.CanAdd;
    public bool CanRemove => _bag.CanRemove;

    public event Action<Item> Added;

    private void OnValidate()
    {
        InterfaceImplementaion.Implement<ISlotView>(ref _slotView);
    }

    public void Init()
    {
        _view.Init(_bag.Capacity);
    }

    public void Add(Item item)
    {
        _bag.Add(item);
        Added?.Invoke(item);
        Slot slot = _view.GetFreeSlot();
        slot.Item = item;
        Transform target = slot.transform;
        item.DisableCollider();
        item.Fly(target);
    }

    public Item Remove()
    {
        Item item = _bag.GetAt(_bag.Items.Count - 1);
        _bag.Remove(item);

        Slot slot = _view.FindSlot(item);
        slot.Item = null;

        return item;
    }

    public void Remove(Item item)
    {
        _bag.Remove(item);

        Slot slot = _view.FindSlot(item);
        slot.Item = null;
    }
}
