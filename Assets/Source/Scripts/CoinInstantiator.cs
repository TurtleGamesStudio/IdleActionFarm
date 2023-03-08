using DG.Tweening;
using Finance;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CoinInstantiator : MonoBehaviour
{
    [SerializeField] private RectTransform _flyingTarget;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Valueable _coinTemplate;
    [SerializeField] private RectTransform _container;
    [SerializeField] private Camera _camera;
    [SerializeField] private Seller _seller;
    [SerializeField] private float _duration = 0.5f;
    [SerializeField] private float _delay = 0.1f;
    [SerializeField] private WalletHolder _walletHolder;

    private Vector2 _screenCenter;
    private Vector2 _modifier;
    private Vector2 _endValue;
    private WaitForSeconds _waiting;

    private void Awake()
    {
        _waiting = new WaitForSeconds(_delay);
        CalculateModifier();
        RectTransform pointInContainer = new GameObject("CoinTarget", typeof(RectTransform)).GetComponent<RectTransform>();
        pointInContainer.position = _flyingTarget.position;
        pointInContainer.transform.parent = _container;
        _endValue = pointInContainer.anchoredPosition;
        Destroy(pointInContainer.gameObject);
    }

    private void OnEnable()
    {
        _seller.Sold += OnSold;
    }

    private void OnDisable()
    {
        _seller.Sold -= OnSold;
    }

    private void OnSold(Valueable valueable)
    {
        StartCoroutine(DelaySpawn(valueable.transform.position, valueable.Value));
    }

    private IEnumerator DelaySpawn(Vector3 worldPosition, int value)
    {
        yield return _waiting;
        Valueable coin = SpawnCoin(worldPosition, value);
        MoveToWallet(coin);
    }

    private Valueable SpawnCoin(Vector3 worldPosition, int value)
    {
        Vector3 spawnPosotion = GetPosition(worldPosition);
        Valueable coin = Instantiate(_coinTemplate, _container.transform);
        coin.transform.localPosition = spawnPosotion;
        coin.transform.localRotation = Quaternion.identity;
        return coin;
    }

    private void MoveToWallet(Valueable coin)
    {
        RectTransform rectTransform = coin.GetComponent<RectTransform>();
        rectTransform.DOAnchorPos(_endValue, _duration).OnComplete(() => OnWalletReached(coin));
    }

    private void OnWalletReached(Valueable coin)
    {
        _walletHolder.PutIn(coin.Value);
        Destroy(coin.gameObject);
    }

    private Vector3 GetPosition(Vector3 worldPosition)
    {
        Vector3 sreenPointPosition = _camera.WorldToScreenPoint(worldPosition);
        return sreenPointPosition * _modifier - _screenCenter;
    }

    private void CalculateModifier()
    {
        RectTransform canvas = _canvas.GetComponent<RectTransform>();
        _screenCenter = canvas.rect.size / 2f;
        _modifier = new Vector2(_screenCenter.x / (Screen.width * 0.5f), _screenCenter.y / (Screen.height * 0.5f));
    }
}
