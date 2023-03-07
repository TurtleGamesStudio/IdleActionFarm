using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagHolder : MonoBehaviour
{
    [SerializeField] private Bag _bag;
    [SerializeField] private MonoBehaviour _bagView;

    private IBagView _view => (IBagView)_bagView;

    private void OnValidate()
    {
        if (_bagView is IBagView == false)
        {
            Debug.LogError($"{nameof(_bagView)} must implement {nameof(IBagView)}");
            _bagView = null;
        }
    }

    public void Init()
    {
        _view.Init(_bag.Capacity);
    }

    public void TryAdd(Item item)
    {
        if (_bag.TryAdd(item))
        {
            Transform target = _view.Places[_bag.Items.Count - 1];
            item.DisableCollider();
            item.Fly(target);
        }
    }
}
