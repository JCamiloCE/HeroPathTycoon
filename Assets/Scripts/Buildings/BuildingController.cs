using Heros;
using GeneralManagers;
using UnityEngine;

namespace Buildings
{
    public class BuildingController : MonoBehaviour, ILifeCycle
    {
        private BuildingHeroProcessor _heroProcessor = null;
        private BuildingArt _buildingArt = null;
        private bool _wasInitialized = false;

        public bool WasInitialized() => _wasInitialized;

        public bool Initialization(params object[] parameters)
        {
            BuildingData buildingData = parameters[0] as BuildingData;
            MapManager mapManager = parameters[1] as MapManager;
            FeatureInGameManager featureInGameManager = parameters[2] as FeatureInGameManager;
            CreateBuilding(mapManager, buildingData, featureInGameManager);
            _wasInitialized = true;
            return true;
        }

        internal void AddHeroToQueue(HeroController heroController)
        {
            _heroProcessor.AddHeroToQueue(heroController);
        }

        private void CreateBuilding(MapManager mapManager, BuildingData buildingData, FeatureInGameManager featureInGameManager)
        {
            transform.position = mapManager.GetPositionForArt(buildingData.GetBuildingType); 
            
            _buildingArt = GetComponent<BuildingArt>();
            bool isBuildingUnlock = IsUnlockBuilding(featureInGameManager, buildingData.GetBuildingType);
            _buildingArt.Initialization(buildingData.GetBuildingInitialSprite, isBuildingUnlock);

            _heroProcessor = GetComponent<BuildingHeroProcessor>();
            _heroProcessor.Initialization(mapManager, _buildingArt, buildingData.GetBuildingTimeToProcess, buildingData.GetBuildingType, buildingData.GetBuildingGoldPerProcess);
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