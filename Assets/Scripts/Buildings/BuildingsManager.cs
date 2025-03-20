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

            //While the prototype we create the Lobby and Barracks
            GameObject objLobby = Instantiate(_buildingControllerBase);
            BuildingController lobby = objLobby.GetComponent<BuildingController>();
            BuildingData data = _buildingsDataScriptableObject.GetBuildingDataByBuildingType(EBuildingType.Lobby);
            lobby.Initialization(data, _mapManager, EBuildingType.Lobby);
            _buildings.Add(EBuildingType.Lobby, lobby);

            GameObject objBarracks = Instantiate(_buildingControllerBase);
            BuildingController barracks = objBarracks.GetComponent<BuildingController>();
            data = _buildingsDataScriptableObject.GetBuildingDataByBuildingType(EBuildingType.Barracks);
            barracks.Initialization(data, _mapManager, EBuildingType.Barracks);
            _buildings.Add(EBuildingType.Barracks, barracks);
        }

        public void AddHeroToBuilding(EBuildingType buildingType, HeroController heroController) 
        {
            _buildings[buildingType].AddHeroToQueue(heroController);
        }
    }
}

