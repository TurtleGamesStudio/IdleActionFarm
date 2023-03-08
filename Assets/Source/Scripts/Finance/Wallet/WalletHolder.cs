using UnityEngine;
using System;

namespace Finance
{
    public class WalletHolder : MonoBehaviour
    {
        private Wallet _wallet;

        public event Action<int> BalanceChanged;

        public int Value => _wallet.Value;

        public void Init()
        {
            _wallet = new Wallet();
            _wallet.BalanceChanged += OnBalanceChanged;
        }

        private void OnDisable()
        {
            _wallet.BalanceChanged -= OnBalanceChanged;
        }

        public void PutIn(int value)
        {
            _wallet.PutIn(value);
        }

        public void Withdraw(int value)
        {
            _wallet.Withdraw(value);
        }

        private void OnBalanceChanged(int value)
        {
            BalanceChanged?.Invoke(value);
        }
    }
}