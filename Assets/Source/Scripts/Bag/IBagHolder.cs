using System;

public interface IBagHolder
{
    public event Action<Item> Added;

    public ISlotView _view { get; }

    public bool CanAdd { get; }
    public bool CanRemove { get; }

    public void Init();
    public void Add(Item item);
    public Item Remove();
    public void Remove(Item item);
}
