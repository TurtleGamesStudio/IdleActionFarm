using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Seller : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _bagHolder;
    [SerializeField] private float _delay = 0.1f;

    private WaitForSeconds _waiting;
    private List<Item> _items;

    public event Action<Valueable> Sold;

    private IBagHolder _holder => (IBagHolder)_bagHolder;

    private void OnValidate()
    {
        InterfaceImplementaion.Implement<IBagHolder>(ref _bagHolder);
    }

    private void Awake()
    {
        _items = new List<Item>();
        _waiting = new WaitForSeconds(_delay);
    }

    private void OnEnable()
    {
        _holder.Added += OnAdded;
    }

    private void OnDisable()
    {
        _holder.Added -= OnAdded;

        foreach (Item item in _items)
        {
            item.Placed -= OnPlaced;
        }
    }

    private void OnAdded(Item item)
    {
        _items.Add(item);
        item.Placed += OnPlaced;
    }

    private void OnPlaced(Item item)
    {
        _items.Remove(item);
        item.Placed -= OnPlaced;
        Sell(item);//for immidiate sell
        //StartCoroutine(Selling(item));//for delayed sell
    }

    private IEnumerator Selling(Item item)
    {
        yield return _waiting;
        Sell(item);
    }

    private void Sell(Item item)
    {
        _holder.Remove(item);

        //Very weak place!
        Valueable valuable = item.GetComponent<Valueable>();

        if (valuable != null)
            Sold?.Invoke(valuable);

        Destroy(item.gameObject);
    }
}
