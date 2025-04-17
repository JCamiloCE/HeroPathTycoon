using HeroPath.Scripts.Enums;
using JCC.Utils.GameplayEventSystem;

namespace HeroPath.Scripts.GameplayEvents 
{
    public class TryToBuyBuildingEvent : EventBase
    {
        public EBuildingType buildingType;
        public int buildingPrice;
        public ECurrency currency;

        public override void SetParameters(params object[] parameters)
        {
            buildingType = (EBuildingType)parameters[0];
            buildingPrice = (int)parameters[1];
            currency = (ECurrency)parameters[2];
        }
    }
}

