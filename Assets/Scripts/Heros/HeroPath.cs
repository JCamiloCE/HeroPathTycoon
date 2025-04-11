using Buildings;
using GeneralManagers;
using System.Collections.Generic;
using UnityEngine;
using Utils.Pool;
using Utils.Random;

namespace Heros
{
    public class HeroPath: ILifeCycle, IPoolResettable
    {
        private struct PointInMap
        {
            public readonly Vector2 positionInMap;
            public readonly EBuildingType buildingType;

            public PointInMap(Vector2 positionInMap, EBuildingType buildingType) 
            {
                this.positionInMap = positionInMap;
                this.buildingType = buildingType;
            }
        }

        private List<PointInMap> _pointsInMap = null;
        private bool _wasInitialized = false;
        private int _currentIndex = -1;
        MapManager _mapManager = null;
        FeatureInGameManager _featureInGameManager = null;
        IRandom _random = null;

        public bool WasInitialized() => _wasInitialized;

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
            _pointsInMap = null;
        }

        public void CreateRandomPath()
        {
            _pointsInMap = new()
            {
                new PointInMap(_mapManager.GetPositionToWait(EBuildingType.Lobby), EBuildingType.Lobby),
                GetFirstLevel(),
                new PointInMap(_mapManager.GetPositionToFinishTraining(), EBuildingType.None)
            };
            _currentIndex = -1;
        }

        public void IterateStep() 
        {
            _currentIndex++;
            if (_currentIndex >= _pointsInMap.Count)
            {
                Debug.Log("Index out of bounds when try to select a the next position");
                _currentIndex = _pointsInMap.Count - 1;
            }
        }

        public Vector2 GetNextPosition() => _pointsInMap[_currentIndex].positionInMap;

        public EBuildingType GetTypeBuilding() => _pointsInMap[_currentIndex].buildingType;

        private PointInMap GetFirstLevel()
        {
            if (!_featureInGameManager.IsFeatureUnlock(EFeatureInGame.FeatureBuildingArcher))
                return new (_mapManager.GetPositionToWait(EBuildingType.Barracks), EBuildingType.Barracks);

            int randomIndex = _random.GetRandomIntBetween(0, 10);
            if (randomIndex < 5)
                return new(_mapManager.GetPositionToWait(EBuildingType.Barracks), EBuildingType.Barracks);
            else
                return new(_mapManager.GetPositionToWait(EBuildingType.Archery), EBuildingType.Archery);
        }
    }
}