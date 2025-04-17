using HeroPath.Scripts.Enums;
using JCC.Utils.GameplayEventSystem;

namespace HeroPath.Scripts.GameplayEvents
{
    public class BuildingPurchasedEvent : EventBase
    {
        public EBuildingType buildingType;

        public override void SetParameters(params object[] parameters)
        {
            buildingType = (EBuildingType)parameters[0];
        }
    }
}