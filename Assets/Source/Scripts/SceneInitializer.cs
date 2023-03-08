using Finance;
using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    [SerializeField] private InputerConstantCanvas _inputerConstantCanvas;
    [SerializeField] private Inputer _inputer;
    [SerializeField] private Player _player;

    [Header("Wallet")]
    [SerializeField] private WalletHolder _walletHolder;
    [SerializeField] private WalletView _walletView;

    private void Start()
    {
        _walletHolder.Init();
        _walletView.Init(_walletHolder);

        _inputerConstantCanvas.Init(_inputer);
        _player.Init(_inputer);
    }
}
