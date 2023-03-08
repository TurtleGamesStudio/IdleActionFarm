using System.Collections.Generic;
using UnityEngine;
using System;

public class BagHolder : MonoBehaviour, IBagHolder
{
    [SerializeField] private Bag _bag;
    [SerializeField] private MonoBehaviour _bagView;

    public ISlotView _view => (ISlotView)_bagView;

    public IReadOnlyList<Item> Items => _bag.Items;
    public bool CanAdd => _bag.CanAdd;
    public bool CanRemove => _bag.CanRemove;

    public event Action<Item> Added;

    private void OnValidate()
    {
        InterfaceImplementaion.Implement<ISlotView>(ref _bagView);
    }

    public void Init()
    {
        _view.Init(_bag.Capacity);
    }

    public void Add(Item item)
    {
        _bag.Add(item);
        Added?.Invoke(item);//be carefull. Call event here mean item added only in bag!!!
        Slot slot = _view.GetFreeSlot();
        slot.Item = item;
        Transform target = slot.transform;
        item.DisableCollider();
        item.Fly(target);
    }

    public Item Remove()
    {
        Item item = _bag.GetAt(_bag.Items.Count - 1);//It's for player bag
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
