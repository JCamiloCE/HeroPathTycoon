using HeroPath.Scripts.Enums;
using JCC.Utils.GameplayEventSystem;
using HeroPath.Scripts.GameplayEvents;
using UnityEngine;
using JCC.Utils.LifeCycle;

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