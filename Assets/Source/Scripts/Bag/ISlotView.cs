public interface ISlotView
{
    public void Init(int capacity);
    public Slot GetFreeSlot();
    public Slot FindSlot(Item item);
}
