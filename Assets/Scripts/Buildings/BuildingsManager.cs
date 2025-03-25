using Heros;
using Map;
using System.Collections.Generic;
using UnityEngine;

namespace Buildings 
{
    public class BuildingsManager : MonoBehaviour
    {
        [SerializeField] private GameObject _buildingControllerBase;
        [SerializeField] private MapManager _mapManager;

        private BuildingsDataScriptableObject _buildingsDataScriptableObject = null;
        private Dictionary<EBuildingType, BuildingController> _buildings;

        private void Awake()
        {
            _buildingsDataScriptableObject = Resources.Load<BuildingsDataScriptableObject>("Scriptables/BuildingsDataScriptableObject");
            CreateInitialBuildings();
        }

        private void CreateInitialBuildings() 
        {
            _buildings = new ();
            CreateSpecificBuilding(EBuildingType.Lobby);
            CreateSpecificBuilding(EBuildingType.Barracks);
        }

        private void CreateSpecificBuilding(EBuildingType buildingType) 
        {
            GameObject gameObjBuilding = Instantiate(_buildingControllerBase);
            BuildingController buildingController = gameObjBuilding.GetComponent<BuildingController>();
            BuildingData data = _buildingsDataScriptableObject.GetBuildingDataByBuildingType(buildingType);
            buildingController.Initialization(data, _mapManager, buildingType);
            _buildings.Add(buildingType, buildingController);
        }

        public void AddHeroToBuilding(EBuildingType buildingType, HeroController heroController) 
        {
            _buildings[buildingType].AddHeroToQueue(heroController);
        }
    }
}

