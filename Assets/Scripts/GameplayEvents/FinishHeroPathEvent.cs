using EvenSystemCore;
using Heros;

namespace HeroPath.Scripts.GameplayEvents
{
    public class FinishHeroPathEvent : EventBase
    {
        public HeroController heroController;

        public override void SetParameters(params object[] parameters)
        {
            heroController = (HeroController)parameters[0];
        }
    }
}