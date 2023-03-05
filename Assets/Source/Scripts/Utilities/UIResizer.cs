using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UIResizer : MonoBehaviour
{
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        UpdateRelativePosition();
        UpdateRelativeScale();
    }

    private void UpdateRelativePosition()
    {
        _rectTransform.anchoredPosition = ResolutionScaler.GetNewAnchoredPosition(_rectTransform.anchoredPosition);
    }

    private void UpdateRelativeScale()
    {
        _rectTransform.localScale = ResolutionScaler.GetNewScale(_rectTransform.localScale);
    }
}
