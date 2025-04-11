using Enums;
using System.Collections.Generic;
using UnityEngine;
using Utils.Random;

namespace GeneralManagers 
{
    public class MapManager : MonoBehaviour
    {
        [System.Serializable]
        private class PositionPerBuilding 
        {
            [SerializeField] private Transform _positionForArt = null;
            [SerializeField] private Transform _positionToWait = null;
            [SerializeField] private Transform _positionToStartQueue = null;

            public PositionPerBuilding(Transform defaultTransfor) 
            {
                _positionForArt = defaultTransfor;
                _positionToWait = defaultTransfor;
                _positionToStartQueue = defaultTransfor;
            }

            public Vector3 GetPositionForArt() => _positionForArt.position;
            public Vector3 GetPositionToWait() => _positionToWait.position;
            public Vector3 GetPositionToStartQueue() => _positionToStartQueue.position;
        }

        [System.Serializable]
        private class PositionWithBuilding
        {
            [SerializeField] private EBuildingType _buildingTypeKey;
            [SerializeField] private PositionPerBuilding _positionPerBuilding;

            public EBuildingType GetBuildingTypeKey() => _buildingTypeKey;
            public PositionPerBuilding GetPositionPerBuilding() => _positionPerBuilding;
        }

        [SerializeField] private List<PositionWithBuilding> _positionsWithKey = null;
        [SerializeField] private Transform _positionToFinishTraining = null;
        [SerializeField] private List<Transform> _spawnPointsHero = null;

        private IRandom _random;

        public Vector3 GetPositionForArt(EBuildingType buildingType) 
        {
            var positions = GetPositionPerBuilding(buildingType);
            return positions.GetPositionForArt();
        }

        public Vector3 GetPositionToWait(EBuildingType buildingType)
        {
            var positions = GetPositionPerBuilding(buildingType);
            return positions.GetPositionToWait();
        }

        public Vector3 GetPositionToStartQueue(EBuildingType buildingType)
        {
            var positions = GetPositionPerBuilding(buildingType);
            return positions.GetPositionToStartQueue();
        }

        private void Awake()
        {
            _random = new RandomUnity();
        }

        private PositionPerBuilding GetPositionPerBuilding(EBuildingType buildingType) 
        {
            int index = _positionsWithKey.FindIndex(x => x.GetBuildingTypeKey() == buildingType);
            if (index < 0)
            {
                Debug.LogError("No found for buildingType: " + buildingType + " Setting as finish point");
                return new PositionPerBuilding(_positionToFinishTraining);
            }
            return _positionsWithKey[index].GetPositionPerBuilding();
        }


        public Vector3 GetPositionToFinishTraining() => _positionToFinishTraining.position;

        public Vector3 SelectHeroSpawnPoint()
        {
            int random_index = _random.GetRandomIndexInList(_spawnPointsHero);
            return _spawnPointsHero[random_index].position;
        }
    }
}

