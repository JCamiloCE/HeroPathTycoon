using HeroPath.Scripts.Enums;
using System;
using UnityEngine;

namespace HeroPath.Scripts.Buildings
{
    [Serializable]
    public class BuildingData
    {
        [SerializeField] private EBuildingType _buildingType;
        [SerializeField] private Sprite _initialSprite;
        [SerializeField] private Sprite _bannerSprite;
        [SerializeField] private float _timeToProcess;
        [SerializeField] private int _goldPerProcess;
        [SerializeField] private int _price;

        public EBuildingType GetBuildingType => _buildingType;
        public Sprite GetBuildingInitialSprite => _initialSprite;
        public Sprite GetBannerBuildingSprite => _bannerSprite;
        public float GetBuildingTimeToProcess => _timeToProcess;
        public int GetBuildingGoldPerProcess => _goldPerProcess;
        public int GetBuildingPrice => _price;
    }
}