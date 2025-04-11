using Heros;
using GeneralManagers;
using UnityEngine;
using UI;
using EvenSystemCore;
using GameplayEvents;

namespace Buildings
{
    public class BuildingController : MonoBehaviour, ILifeCycle, IEventListener<UnlockFeatureEvent>
    {
        [SerializeField] private UIBuildingController _UIBuildingController;

        private BuildingHeroProcessor _heroProcessor = null;
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
            _heroProcessor.AddHeroToQueue(heroController);
        }

        void IEventListener<UnlockFeatureEvent>.OnEvent(UnlockFeatureEvent event_data)
        {
            switch (event_data.featureInGame)
            {
                case EFeatureInGame.FeatureBuildingArcher:
                    if (_buildingType == EBuildingType.Archery)
                    {
                        _buildingArt.UnlockBuilding();
                    }
                    break;
                case EFeatureInGame.FeatureBuildingBarracks:
                    if (_buildingType == EBuildingType.Barracks)
                    {
                        _buildingArt.UnlockBuilding();
                    }
                    break;
            }
        }

        private void CreateBuilding(MapManager mapManager, BuildingData buildingData, FeatureInGameManager featureInGameManager)
        {
            transform.position = mapManager.GetPositionForArt(buildingData.GetBuildingType); 
            
            _buildingArt = GetComponent<BuildingArt>();
            bool isBuildingUnlock = IsUnlockBuilding(featureInGameManager, buildingData.GetBuildingType);
            _buildingArt.Initialization(buildingData.GetBuildingInitialSprite, isBuildingUnlock);

            _heroProcessor = GetComponent<BuildingHeroProcessor>();
            _heroProcessor.Initialization(mapManager, _buildingArt, buildingData.GetBuildingTimeToProcess, buildingData.GetBuildingType, buildingData.GetBuildingGoldPerProcess);

            _UIBuildingController.Initialization(buildingData);
        }

        private bool IsUnlockBuilding(FeatureInGameManager featureInGameManager, EBuildingType buildingType) 
        {
            switch (buildingType)
            {
                case EBuildingType.Archery:
                    return featureInGameManager.IsFeatureUnlock(EFeatureInGame.FeatureBuildingArcher);
                case EBuildingType.Barracks:
                    return featureInGameManager.IsFeatureUnlock(EFeatureInGame.FeatureBuildingBarracks);
                case EBuildingType.Lobby:
                    return featureInGameManager.IsFeatureUnlock(EFeatureInGame.FeatureBuildingLobby);

                case EBuildingType.Invalid:
                case EBuildingType.None:
                default:
                    Debug.LogError("Unsuppor Building type: " + buildingType);
                    return false;
                    break;
            }
        }


    }
}