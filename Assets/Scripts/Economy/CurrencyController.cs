using Enums;
using EvenSystemCore;
using GameplayEvents;
using System.Collections.Generic;
using UnityEngine;

namespace Economy 
{
    public class CurrencyController : MonoBehaviour, IEventListener<UserCurrencyChangeEvent>
    {
        private Dictionary<ECurrency, int> _currentAmounts = null;

        private void Awake()
        {
            _currentAmounts = new()
            {
                { ECurrency.Soft, 0 }
            };

            EventManager.AddListener(this);
        }

        void IEventListener<UserCurrencyChangeEvent>.OnEvent(UserCurrencyChangeEvent event_data)
        {
            if (_currentAmounts.ContainsKey(event_data.currencyChanged)) 
            {
                _currentAmounts[event_data.currencyChanged] += event_data.delta;
                EventManager.TriggerEvent<UserCurrencyUIChangeEvent>(event_data.currencyChanged, _currentAmounts[event_data.currencyChanged]);
            }
        }
    }
}

