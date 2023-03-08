using System;
using UnityEngine;

namespace Finance
{
    public class Wallet
    {
        private const string Id = "Wallet";
        private int _value;

        public event Action<int> BalanceChanged;
        public event Action<int> Setted;

        public const int MaxValue = int.MaxValue;

        public int Value => _value;

        public Wallet()
        {
            _value = PlayerPrefs.GetInt(Id, 0);
        }

        public void Set(int value)
        {
            if (value < 0)
            {
                throw new ArgumentException($"{nameof(value)} must be non negative", nameof(value));
            }

            _value = value;
            PlayerPrefs.SetInt(Id, _value);
            Setted?.Invoke(_value);
        }

        public void PutIn(int value)
        {
            if (value < 0)
            {
                throw new ArgumentException($"{nameof(value)} must be non negative", nameof(value));
            }

            _value += value;
            PlayerPrefs.SetInt(Id, _value);
            BalanceChanged?.Invoke(_value);
        }

        public void Withdraw(int value)
        {
            if (value < 0)
            {
                throw new ArgumentException($"{nameof(value)} must be non negative", nameof(value));
            }

            if (value > _value)
            {
                throw new ArgumentException($"You try withdraw more than {nameof(Wallet)} have", nameof(value));
            }

            _value -= value;
            PlayerPrefs.SetInt(Id, _value);
            BalanceChanged?.Invoke(_value);
        }
    }
}