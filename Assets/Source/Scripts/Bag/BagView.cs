using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class BagView : MonoBehaviour
{
    [SerializeField] private Bag _bag;
    private TMP_Text _text;

    private void OnDisable()
    {
        _bag.Added -= OnAdded;
        _bag.Removed -= OnRemoved;
    }

    public void Init()
    {
        _text = GetComponent<TMP_Text>();
        _bag.Added += OnAdded;
        _bag.Removed += OnRemoved;
        Show();
    }

    private void OnAdded(Item _)
    {
        Show();
    }

    private void OnRemoved(Item _)
    {
        Show();
    }

    private void Show()
    {
        _text.text = _bag.Items.Count.ToString() + "/" + _bag.Capacity;
    }
}
