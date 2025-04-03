using Heros;
using Map;
using UnityEngine;

namespace Buildings
{
    public class BuildingController : MonoBehaviour, ILifeCycle
    {
        private BuildingHeroProcessor _heroProcessor = null;
        private BuildingArt _buildingArt = null;
        private bool _wasInitialized = false;
        private EBuildingType _buildingType = EBuildingType.None;

        public bool WasInitialized() => _wasInitialized;

        public bool Initialization(params object[] parameters)
        {
            BuildingData buildingData = parameters[0] as BuildingData;
            MapManager mapManager = parameters[1] as MapManager;
            _buildingType = (EBuildingType)parameters[2];
            CreateBuilding(mapManager, buildingData);
            _wasInitialized = true;
            return true;
        }

        internal void AddHeroToQueue(HeroController heroController)
        {
            _heroProcessor.AddHeroToQueue(heroController);
        }

        private void CreateBuilding(MapManager mapManager, BuildingData buildingData)
        {
            transform.position = GetInitialPosition(mapManager);
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = buildingData.GetBuildingInitialSprite;

            _buildingArt = GetComponent<BuildingArt>();
            _buildingArt.Initialization();

            _heroProcessor = GetComponent<BuildingHeroProcessor>();
            _heroProcessor.Initialization(mapManager, _buildingArt, buildingData.GetBuildingTimeToProcess);
        }

        private Vector3 GetInitialPosition(MapManager mapManager) 
        {
            switch (_buildingType) 
            {
                case EBuildingType.Lobby:
                    return mapManager.GetPositionForLobby();

                case EBuildingType.Barracks:
                    return mapManager.GetPositionForBarracks();

                default:
                    Debug.LogError("BuildingController.GetInitialPosition: not found intial position");
                    return Vector3.zero;

            }
        }
    }
}