using Enums;
using EvenSystemCore;
using Heros;

namespace GameplayEvents 
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

