using EvenSystemCore;
using HeroPath.Scripts.GameplayEvents;
using GeneralManagers;
using System.Collections.Generic;
using UnityEngine;
using HeroPath.Scripts.Enums;

namespace HeroPath.Scripts.Buildings 
{
    public class BuildingsManager : MonoBehaviour, IEventListener<StartProcessForHeroEvent>
    {
        [SerializeField] private GameObject _buildingControllerBase;
        [SerializeField] private MapManager _mapManager;
        [SerializeField] private FeatureInGameManager _featureInGameManager;

        private BuildingsDataScriptableObject _buildingsDataScriptableObject = null;
        private Dictionary<EBuildingType, BuildingController> _buildings;

        void IEventListener<StartProcessForHeroEvent>.OnEvent(StartProcessForHeroEvent event_data)
        {
            _buildings[event_data.buildingType].AddHeroToQueue(event_data.heroController);
        }

        private void Awake()
        {
            _buildingsDataScriptableObject = Resources.Load<BuildingsDataScriptableObject>("Scriptables/BuildingsDataScriptableObject");
            EventManager.AddListener(this);
            CreateInitialBuildings();
        }

        private void CreateInitialBuildings() 
        {
            _buildings = new ();
            HashSet<EBuildingType> buildingTypes = _buildingsDataScriptableObject.GetAllTypeOfBuildings();
            foreach (EBuildingType buildingType in buildingTypes)
            {
                CreateSpecificBuilding(buildingType);
            }
        }

        private void CreateSpecificBuilding(EBuildingType buildingType) 
        {
            GameObject gameObjBuilding = Instantiate(_buildingControllerBase);
            BuildingController buildingController = gameObjBuilding.GetComponent<BuildingController>();
            BuildingData data = _buildingsDataScriptableObject.GetBuildingDataByBuildingType(buildingType);
            buildingController.Initialization(data, _mapManager, _featureInGameManager);
            _buildings.Add(buildingType, buildingController);
        }
    }
}
