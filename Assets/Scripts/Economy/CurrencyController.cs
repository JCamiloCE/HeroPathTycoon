using Enums;
using EvenSystemCore;
using GameplayEvents;
using System.Collections.Generic;
using UnityEngine;

namespace Economy 
{
    public class CurrencyController : MonoBehaviour, IEventListener<UserCurrencyChangeEvent>,
                                                     IEventListener<TryToBuyBuildingEvent>

    {
        private Dictionary<ECurrency, int> _currentAmounts = null;

        private void Awake()
        {
            _currentAmounts = new()
            {
                { ECurrency.Soft, 0 }
            };

            EventManager.AddListener<UserCurrencyChangeEvent>(this);
            EventManager.AddListener<TryToBuyBuildingEvent>(this);
        }

        void IEventListener<UserCurrencyChangeEvent>.OnEvent(UserCurrencyChangeEvent event_data)
        {
            if (_currentAmounts.ContainsKey(event_data.currencyChanged)) 
            {
                _currentAmounts[event_data.currencyChanged] += event_data.delta;
                EventManager.TriggerEvent<UserCurrencyUIChangeEvent>(event_data.currencyChanged, _currentAmounts[event_data.currencyChanged]);
            }
        }

        void IEventListener<TryToBuyBuildingEvent>.OnEvent(TryToBuyBuildingEvent event_data)
        {
            if (_currentAmounts.ContainsKey(event_data.currency))
            {
                if (_currentAmounts[event_data.currency] >= event_data.buildingPrice) 
                {
                    _currentAmounts[event_data.currency] -= event_data.buildingPrice;
                    EventManager.TriggerEvent<UserCurrencyUIChangeEvent>(event_data.currency, _currentAmounts[event_data.currency]);
                    EventManager.TriggerEvent<BuildingPurchasedEvent>(event_data.buildingType);
                }
            }
        }
    }
}

