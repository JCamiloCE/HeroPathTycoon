using HeroPath.Scripts.Enums;
using JCC.Utils.GameplayEventSystem;
using HeroPath.Scripts.Heros;

namespace HeroPath.Scripts.GameplayEvents 
{
    public class StartProcessForHeroEvent : EventBase
    {
        public HeroController heroController;
        public EBuildingType buildingType;

        public override void SetParameters(params object[] parameters)
        {
            heroController = (HeroController)parameters[0];
            buildingType = (EBuildingType)parameters[1];
        }
    }
}

