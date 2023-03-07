using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private BagHolder _bagHolder;

    private void Awake()
    {
        _bagHolder.Init();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Item item))
        {
            _bagHolder.TryAdd(item);
        }
    }
}
