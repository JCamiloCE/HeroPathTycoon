using EvenSystemCore;
using Heros;

namespace GameplayEvents
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