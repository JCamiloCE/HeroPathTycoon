using HeroPath.Scripts.Heros;
using HeroPath.Scripts.UI;
using HeroPath.Scripts.GeneralManagers;
using HeroPath.Scripts.GameplayEvents;
using HeroPath.Scripts.Enums;
using JCC.Utils.GameplayEventSystem;
using JCC.Utils.LifeCycle;
using UnityEngine;

namespace HeroPath.Scripts.Buildings
{
    public class BuildingController : MonoBehaviour, ILifeCycle, IEventListener<UnlockFeatureEvent>
    {
        [SerializeField] private UIBuildingController _UIBuildingController;

        private BuildingHeroProcessor _buildingHeroProcessor = null;
        private BuildingArt _buildingArt = null;
        private EBuildingType _buildingType;
        private bool _wasInitialized = false;

        public bool WasInitialized() => _wasInitialized;

        public bool Initialization(params object[] parameters)
        {
            BuildingData buildingData = parameters[0] as BuildingData;
            MapManager mapManager = parameters[1] as MapManager;
            FeatureInGameManager featureInGameManager = parameters[2] as FeatureInGameManager;
            CreateBuilding(mapManager, buildingData, featureInGameManager);
            _buildingType = buildingData.GetBuildingType;
            EventManager.AddListener<UnlockFeatureEvent>(this);
            _wasInitialized = true;
            return true;
        }

        internal void AddHeroToQueue(HeroController heroController)
        {
            _buildingHeroProcessor.AddHeroToQueue(heroController);
        }

        void IEventListener<UnlockFeatureEvent>.OnEvent(UnlockFeatureEvent event_data)
        {
            if (_buildingType == EnumConverter.GetBuildingByFeature(event_data.featureInGame))
            {
                _buildingArt.UnlockBuilding();
            }
        }

        private void CreateBuilding(MapManager mapManager, BuildingData buildingData, FeatureInGameManager featureInGameManager)
        {
            transform.position = mapManager.GetPositionByBuilding(buildingData.GetBuildingType, EMapTypePosition.ForArt);
            CreateBuildingArt(buildingData, featureInGameManager);
            CreateBuildingHeroProcessor(mapManager, buildingData);
            _UIBuildingController.Initialization(buildingData.GetBuildingType, buildingData.GetBuildingPrice);
        }

        private void CreateBuildingArt(BuildingData buildingData, FeatureInGameManager featureInGameManager) 
        {
            _buildingArt = GetComponent<BuildingArt>();
            bool isBuildingUnlock = featureInGameManager.IsFeatureUnlock(EnumConverter.GetFeatureByBuilding(buildingData.GetBuildingType));
            _buildingArt.Initialization(buildingData.GetBuildingInitialSprite, buildingData.GetBannerBuildingSprite, isBuildingUnlock);
        }

        private void CreateBuildingHeroProcessor(MapManager mapManager, BuildingData buildingData)
        {
            _buildingHeroProcessor = GetComponent<BuildingHeroProcessor>();
            _buildingHeroProcessor.Initialization(mapManager, _buildingArt, buildingData);
        }
    }
}