using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class CircleOfInput : MonoBehaviour
{
    private Inputer _inputer;
    private RectTransform _rectTransform;
    private float _radius;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.sizeDelta = _inputer.Radius * 2 * Vector2.one;
    }

    private void OnDisable()
    {
        Reset();
    }

    private void Update()
    {
        _rectTransform.anchoredPosition = _inputer.Direction * _inputer.Share * _radius;
    }

    private void Reset()
    {
        _rectTransform.anchoredPosition = Vector2.zero;
    }

    public void Init(Inputer inputer)
    {
        _inputer = inputer;
        _radius = inputer.Radius;
    }
}