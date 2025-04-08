using Buildings;
using GeneralManagers;
using System.Collections.Generic;
using UnityEngine;
using Utils.Random;

namespace Heros
{
    public class HeroPath: ILifeCycle
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

        public bool WasInitialized() => _wasInitialized;

        public bool Initialization(params object[] parameters)
        {
            MapManager mapManager = parameters[0] as MapManager;
            FeatureInGameManager featureInGameManager = parameters[1] as FeatureInGameManager;
            IRandom random = parameters[2] as IRandom;

            CreateRandomPath(mapManager, featureInGameManager, random);
            _wasInitialized = true;
            return _wasInitialized;
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

        private void CreateRandomPath(MapManager mapManager, FeatureInGameManager featureInGameManager, IRandom random)
        {
            _pointsInMap = new()
            {
                new PointInMap(mapManager.GetPositionToWait(EBuildingType.Lobby), EBuildingType.Lobby),
                GetFirstLevel(mapManager, featureInGameManager, random),
                new PointInMap(mapManager.GetPositionToFinishTraining(), EBuildingType.None)
            };
            _currentIndex = -1;
        }

        private PointInMap GetFirstLevel(MapManager mapManager, FeatureInGameManager featureInGameManager, IRandom random)
        {
            if (!featureInGameManager.IsFeatureUnlock(EFeatureInGame.FeatureBuildingArcher))
                return new (mapManager.GetPositionToWait(EBuildingType.Barracks), EBuildingType.Barracks);

            int randomIndex = random.GetRandomIntBetween(0, 10);
            if (randomIndex < 5)
                return new(mapManager.GetPositionToWait(EBuildingType.Lobby), EBuildingType.Barracks);

            return new(mapManager.GetPositionToWait(EBuildingType.Lobby), EBuildingType.Lobby); //Temp
        }
    }
}