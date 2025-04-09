using EvenSystemCore;
using GameplayEvents;
using System.Collections;
using TMPro;
using UnityEngine;

namespace UI 
{
    public class UIGoldCurrencyUpdater : MonoBehaviour, IEventListener<UserCurrencyUIChangeEvent>
    {
        [SerializeField] private TextMeshProUGUI _textCurrency = null;
        [SerializeField] private float _normalSize = 110;
        [SerializeField] private float _bigSize = 120;

        private Coroutine _effectCurrencyChange = null;
        private int _newValue = 0;
        private int _temporalNewValue = 0;

        private void Awake()
        {
            _textCurrency.text = $"${_temporalNewValue}";
            EventManager.AddListener(this);
        }

        void IEventListener<UserCurrencyUIChangeEvent>.OnEvent(UserCurrencyUIChangeEvent event_data)
        {
            if (event_data.currencyChanged == Enums.ECurrency.Soft) 
            {
                StopEffect();
                _effectCurrencyChange = StartCoroutine(EffectCurrencyChange(event_data.newValue));
            }
        }

        private void StopEffect() 
        {
            if (_effectCurrencyChange != null) 
            {
                StopCoroutine(_effectCurrencyChange);
                _effectCurrencyChange = null;
            }
        }

        private IEnumerator EffectCurrencyChange(float newValue) 
        {
            float duration = 0.2f;
            float currentTime = 0f;
            float initialCurrencyValue = _temporalNewValue;

            while (currentTime < duration) 
            {
                float size = Mathf.Lerp(_normalSize, _bigSize, currentTime / duration);
                _textCurrency.fontSize = size;
                _temporalNewValue = Mathf.RoundToInt(Mathf.Lerp(initialCurrencyValue, newValue, currentTime / duration));
                _textCurrency.text = $"${_temporalNewValue}";
                currentTime += Time.deltaTime;
                yield return null;
            }

            _textCurrency.fontSize = _bigSize;
            _textCurrency.text = $"${_temporalNewValue}";
            yield return new WaitForSeconds(0.2f);
            currentTime = 0f;

            while (currentTime < duration)
            {
                float size = Mathf.Lerp(_bigSize, _normalSize, currentTime / duration);
                _textCurrency.fontSize = size;
                currentTime += Time.deltaTime;
                yield return null;
            }

            _textCurrency.fontSize = _normalSize;
            StopEffect();
        }
    }
}

