using Map;
using UnityEngine;

namespace Buildings 
{
    public class BuildingsManager : MonoBehaviour
    {
        [SerializeField] private MapManager _mapManager;

        private BuildingsDataScriptableObject _buildingsDataScriptableObject = null;

        private void Awake()
        {
            _buildingsDataScriptableObject = Resources.Load<BuildingsDataScriptableObject>("Scriptables/BuildingsDataScriptableObject");
            CreateInitialBuildings();
        }

        private void CreateInitialBuildings() 
        {
            //While the prototype we create the Lobby and Barracks
            BuildingController lobby = new BuildingController();
            BuildingData data = _buildingsDataScriptableObject.GetBuildingDataByBuildingType(EBuildingType.Lobby);
            lobby.Initialization(data, _mapManager.GetPositionForLobby());

            BuildingController barracks = new BuildingController();
            data = _buildingsDataScriptableObject.GetBuildingDataByBuildingType(EBuildingType.Barracks);
            barracks.Initialization(data, _mapManager.GetPositionForBarracks());
        }
    }
}

