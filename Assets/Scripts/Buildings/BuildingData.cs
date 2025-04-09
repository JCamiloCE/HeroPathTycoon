using System;
using UnityEngine;

namespace Buildings
{
    [Serializable]
    public class BuildingData
    {
        [SerializeField] private EBuildingType _buildingType;
        [SerializeField] private Sprite _initialSprite;
        [SerializeField] private float _timeToProcess;
        [SerializeField] private int _goldPerProcess;

        public EBuildingType GetBuildingType => _buildingType;
        public Sprite GetBuildingInitialSprite => _initialSprite;
        public float GetBuildingTimeToProcess => _timeToProcess;
        public int GetBuildingGoldPerProcess => _goldPerProcess;
    }
}