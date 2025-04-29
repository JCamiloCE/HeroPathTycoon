using HeroPath.Scripts.Enums;
using HeroPath.Scripts.GeneralManagers;
using JCC.Utils.DebugManager;
using JCC.Utils.LifeCycle;
using JCC.Utils.Pool;
using JCC.Utils.Random;
using System.Collections.Generic;
using UnityEngine;

namespace HeroPath.Scripts.Heros
{
    public class HeroPath: ILifeCycle, IPoolResettable
    {
        private List<EBuildingType> _heroPath = null;
        private bool _wasInitialized = false;
        private int _currentIndex = -1;
        MapManager _mapManager = null;
        FeatureInGameManager _featureInGameManager = null;
        IRandom _random = null;

        public bool WasInitialized() => _wasInitialized;

        public EBuildingType GetTypeBuilding() => _heroPath[_currentIndex];

        public bool Initialization(params object[] parameters)
        {
            _mapManager = parameters[0] as MapManager;
            _featureInGameManager = parameters[1] as FeatureInGameManager;
            _random = parameters[2] as IRandom;
            _wasInitialized = true;
            return _wasInitialized;
        }

        void IPoolResettable.ResetPoolObject()
        {
            _currentIndex = -1;
            _heroPath = null;
        }

        public void CreateRandomPath()
        {
            _heroPath = new()
            {
                EBuildingType.Lobby,
                GetFirstLevelOfPath(),
                EBuildingType.None
            };
            _currentIndex = -1;
        }

        public void IterateStep() 
        {
            _currentIndex++;
            if (_currentIndex >= _heroPath.Count)
            {
                DebugManager.LogError("Index out of bounds when try to select a the next position");
                _currentIndex = _heroPath.Count - 1;
            }
        }

        public Vector2 GetNextPosition()
        {
            if (_heroPath[_currentIndex] == EBuildingType.None)
            {
                return _mapManager.GetPositionToFinishTraining();
            }

            return _mapManager.GetPositionByBuilding(_heroPath[_currentIndex], EMapTypePosition.ToWait);
        }

        private EBuildingType GetFirstLevelOfPath()
        {
            if (!_featureInGameManager.IsFeatureUnlock(EFeatureInGame.FeatureBuildingArcher))
                return EBuildingType.Barracks;

            int randomIndex = _random.GetRandomIntBetween(0, 10);
            if (randomIndex < 5)
                return EBuildingType.Barracks;
            else
                return EBuildingType.Archery;
        }
    }
}