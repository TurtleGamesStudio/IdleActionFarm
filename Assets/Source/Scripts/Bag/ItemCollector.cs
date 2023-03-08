using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private BagHolder _bagHolder;

    private void Awake()
    {
        _bagHolder.Init();
    }

    private void OnTriggerStay(Collider other)
    {
        if (_bagHolder.CanAdd && other.TryGetComponent(out Item item))
        {
            _bagHolder.Add(item);
        }
    }
}
