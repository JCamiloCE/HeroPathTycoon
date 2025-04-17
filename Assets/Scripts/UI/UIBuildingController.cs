using HeroPath.Scripts.Enums;
using EvenSystemCore;
using HeroPath.Scripts.GameplayEvents;
using UnityEngine;

namespace HeroPath.Scripts.UI
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
            _buildingType = (EBuildingType)parameters[0];
            _buildingPrice = (int)parameters[1];
            return true;
        }

        public void TryToBuyBuilding() 
        {
            EventManager.TriggerEvent<TryToBuyBuildingEvent>(_buildingType, _buildingPrice, ECurrency.Soft);
        }
    }
}