using UnityEngine;
using TMPro;
using System.Collections;

namespace Finance
{
    [RequireComponent(typeof(TMP_Text))]
    public class WalletView : MonoBehaviour
    {
        private int Vibrate = Animator.StringToHash("Vibrate");

        [SerializeField] private AnimationCurve _countDurationDependency;
        [SerializeField] private Animator _animator;

        private TMP_Text _text;
        private int _value;
        private Coroutine _valueChangingCoroutine;
        private WalletHolder _walletHolder;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        public void Init(WalletHolder walletHolder)
        {
            _walletHolder = walletHolder;
            _value = walletHolder.Value;
            _text.text = _value.ToString();
            walletHolder.BalanceChanged += OnBalanceChanged;
        }

        private void OnDisable()
        {
            if (_valueChangingCoroutine != null)
            {
                StopCoroutine(_valueChangingCoroutine);
            }

            _walletHolder.BalanceChanged -= OnBalanceChanged;
        }

        private void SetValue(int value)
        {
            _value = value;
            _text.text = _value.ToString();
        }

        private void OnBalanceChanged(int target)
        {
            if (_valueChangingCoroutine != null)
            {
                StopCoroutine(_valueChangingCoroutine);
            }

            _valueChangingCoroutine = StartCoroutine(Change(target));
        }

        private IEnumerator Change(int target)
        {
            _animator.SetBool(Vibrate, true);

            int startValue = _value;
            int difference = target - _value;
            int distance = Mathf.Abs(target - _value);

            float animationTime = _countDurationDependency.Evaluate(distance);

            float time = 0;

            while (time < animationTime)
            {
                time += Time.deltaTime;
                float progress = time / animationTime;
                _value = startValue + MyMathf.Evaluate(difference, progress);
                _text.text = _value.ToString();

                yield return null;
            }

            _animator.SetBool(Vibrate, false);
        }
    }
}