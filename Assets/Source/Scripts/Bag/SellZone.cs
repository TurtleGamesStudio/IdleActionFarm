using UnityEngine;

public class SellZone : MonoBehaviour
{
    [SerializeField] private BagHolder _bagHolder;

    private void Awake()
    {
        _bagHolder.Init();
    }

    private void OnTriggerStay(Collider other)
    {
        if (_bagHolder.CanAdd && other.TryGetComponent(out Player player))
        {
            if (player.BagHolder.CanRemove)
            {
                Item item = player.BagHolder.Remove();
                _bagHolder.Add(item);
            }
        }
    }
}
