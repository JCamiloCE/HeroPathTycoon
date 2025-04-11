using Buildings;
using Enums;
using EvenSystemCore;
using GameplayEvents;
using UnityEngine;

namespace UI
{
    public class UIBuildingController : MonoBehaviour, ILifeCycle
    {
        private EBuildingType _buildingType = EBuildingType.Invalid;
        private int _buildingPrice = 0;

        public bool WasInitialized()
        {
            throw new System.NotImplementedException();
        }

        public bool Initialization(params object[] parameters)
        {
            BuildingData buildingData = parameters[0] as BuildingData;
            _buildingType = buildingData.GetBuildingType;
            _buildingPrice = buildingData.GetBuildingPrice;
            return true;
        }

        public void TryToBuyBuilding() 
        {
            EventManager.TriggerEvent<TryToBuyBuildingEvent>(_buildingType, _buildingPrice, ECurrency.Soft);
        }

        
    }
}