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
            transform.position = mapManager.GetPositionForArt(buildingData.GetBuildingType); 
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = buildingData.GetBuildingInitialSprite;

            _buildingArt = GetComponent<BuildingArt>();
            _buildingArt.Initialization();

            _heroProcessor = GetComponent<BuildingHeroProcessor>();
            _heroProcessor.Initialization(mapManager, _buildingArt, buildingData.GetBuildingTimeToProcess, buildingData.GetBuildingType, buildingData.GetBuildingGoldPerProcess);
        }
    }
}