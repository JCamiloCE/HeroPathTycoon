using System;
using UnityEngine;
using Enums;

namespace Buildings
{
    [Serializable]
    public class BuildingData
    {
        [SerializeField] private EBuildingType _buildingType;
        [SerializeField] private Sprite _initialSprite;
        [SerializeField] private float _timeToProcess;
        [SerializeField] private int _goldPerProcess;
        [SerializeField] private int _price;

        public EBuildingType GetBuildingType => _buildingType;
        public Sprite GetBuildingInitialSprite => _initialSprite;
        public float GetBuildingTimeToProcess => _timeToProcess;
        public int GetBuildingGoldPerProcess => _goldPerProcess;
        public int GetBuildingPrice => _price;
    }
}