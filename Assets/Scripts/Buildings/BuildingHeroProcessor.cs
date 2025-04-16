using Heros;
using GeneralManagers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EvenSystemCore;
using GameplayEvents;
using HeroPath.Scripts.Enums;

namespace HeroPath.Scripts.Buildings
{
    public class BuildingHeroProcessor : MonoBehaviour, ILifeCycle
    {
        private List<HeroController> _heroControllers = null;
        private HeroController _currentHero = null; //This is temporal the idea is the building doesn't contains Hero reference
        private Coroutine _heroProcessorCoroutine = null;
        private MapManager _mapManager = null;
        private BuildingArt _buildingArt = null;
        private EBuildingType _buildingType = EBuildingType.None;
        private float _timeToProcess = 1;
        private bool _wasInitialized = false;
        private int _currencyPerProcess = 1;

        public bool WasInitialized() => _wasInitialized;

        public bool Initialization(params object[] parameters)
        {
            _mapManager = parameters[0] as MapManager;
            _buildingArt = parameters[1] as BuildingArt;
            BuildingData buildingData = parameters[2] as BuildingData;
            _timeToProcess = buildingData.GetBuildingTimeToProcess;
            _buildingType = buildingData.GetBuildingType;
            _currencyPerProcess = buildingData.GetBuildingGoldPerProcess;
            _heroControllers = new ();
            _wasInitialized = true;
            return _wasInitialized;
        }

        internal void AddHeroToQueue(HeroController heroController)
        {
            _heroControllers.Add(heroController);
            SetNewPositionForQueue(_currentHero != null);
        }

        private void SetNewPositionForQueue(bool isProcessHero) 
        {
            for (int i = 0; i < _heroControllers.Count; i++)
            {
                if (isProcessHero)
                    _heroControllers[i].MoveToNewPoint(GetPositionByIndex(i + 1), null);
                else
                {
                    if (i == 0)
                    {
                        _heroControllers[i].MoveToNewPoint(GetPositionByIndex(i), OnFinishHeroMovement);
                        _heroControllers[i].StartFadeOut(1f, overrideFade:false);
                    }
                    else 
                    {
                        _heroControllers[i].MoveToNewPoint(GetPositionByIndex(i), null);
                    }
                }
            }
        }

        private Vector3 GetPositionByIndex(int index) 
        {
            Vector3 newPosition = _mapManager.GetPositionToStartQueue(_buildingType);
            newPosition.y -= (index * 2);
            return newPosition;
        }

        private void OnFinishHeroMovement() 
        {
            _currentHero = _heroControllers[0];
            _heroControllers.RemoveAt(0);
            RunProcessor();
        }

        private void RunProcessor()
        {
            if (_heroProcessorCoroutine == null)
            {
                _heroProcessorCoroutine = StartCoroutine(ProcessHero());
            }
            else 
            {
                Debug.LogError("BuildingHeroProcessor.TryRunProcessor: try to process two heros at the same time");
            }
        }

        private IEnumerator ProcessHero() 
        {
            while(_currentHero != null) 
            {
                _buildingArt.StartProcess(_timeToProcess);
                yield return new WaitForSeconds(_timeToProcess);
                EvolveHero();
                _currentHero = null;
            }
            SetNewPositionForQueue(_currentHero != null);
            EventManager.TriggerEvent<UserCurrencyChangeEvent>(ECurrency.Soft, _currencyPerProcess);
            _heroProcessorCoroutine = null;
        }

        private void EvolveHero() 
        {
            _currentHero.StartFadeIn(1f, overrideFade: true);
            _currentHero.EvolveHero(EnumConverter.GetHeroFamilyByBuilding(_buildingType));
            _currentHero.CallNextStepInHeroPath();
        }
    }
}