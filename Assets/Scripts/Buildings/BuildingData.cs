using System;
using UnityEngine;

namespace Buildings
{
    [Serializable]
    public class BuildingData
    {
        [SerializeField] private EBuildingType _buildingType;
        [SerializeField] private Sprite _initialSprite;

        public EBuildingType GetBuildingType => _buildingType;
        public Sprite GetBuildingInitialSprite => _initialSprite;
    }
}