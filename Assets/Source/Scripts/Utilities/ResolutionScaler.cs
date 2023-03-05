using UnityEngine;

public static class ResolutionScaler
{
    private static Vector2Int _originalResolution;

    private static float _orginalRatio;
    private static float _currentRatio;
    private static float _ratioOfRatios;

    private static float _heightMultiplier;
    private static float _widthMultiplier;

    private static float _minimalMultiplier;

    static ResolutionScaler()
    {
        Init();
    }

    private static void Init()
    {
        _originalResolution = new Vector2Int(1024, 1280);
        CalculateOrginalRatio();
        CalculateCurrentRatio();
        CalculateRatioOfRatios();

        CalculateHeightMultiplier();
        CalculateWidthMultiplier();
        CalculateMinimalMultiplier();
    }

    private static void CalculateOrginalRatio()
    {
        _orginalRatio = (float)_originalResolution.x / _originalResolution.y;
    }

    private static void CalculateCurrentRatio()
    {
        _currentRatio = (float)Screen.width / Screen.height;
    }

    private static void CalculateRatioOfRatios()
    {
        _ratioOfRatios = _currentRatio / _orginalRatio;
    }

    private static void CalculateHeightMultiplier()
    {
        _heightMultiplier = (float)Screen.height / _originalResolution.y;
    }

    private static void CalculateWidthMultiplier()
    {
        _widthMultiplier = (float)Screen.width / _originalResolution.x;
    }

    private static void CalculateMinimalMultiplier()
    {
        if (_heightMultiplier < _widthMultiplier)
        {
            _minimalMultiplier = _heightMultiplier;
        }
        else
        {
            _minimalMultiplier = _widthMultiplier;
        }
    }

    public static Vector2 GetPosition(Vector2 oldPosition)
    {
        Vector2 newPosition = new Vector2(oldPosition.x * _ratioOfRatios, oldPosition.y);
        return newPosition;
    }

    public static Vector3 SaveScaleProportion(Vector3 oldScale)
    {
        if (_ratioOfRatios > 1)
        {
            return oldScale;
        }
        else
        {
            return oldScale * _ratioOfRatios;
        }
    }

    public static Vector2 SaveRelativeWidth(Vector2 oldScale)
    {
        return new Vector2(oldScale.x * _ratioOfRatios, oldScale.y);
    }

    public static Vector2 GetNewScale(Vector2 oldScale)
    {
        return oldScale * _minimalMultiplier;
    }

    public static Vector3 GetNewScale(Vector3 oldScale)
    {
        return oldScale * _minimalMultiplier;
    }

    public static Vector2 GetNewAnchoredPosition(Vector2 oldAncharedPosition)
    {
        return new Vector2(oldAncharedPosition.x * _widthMultiplier, oldAncharedPosition.y * _heightMultiplier);
    }
}
