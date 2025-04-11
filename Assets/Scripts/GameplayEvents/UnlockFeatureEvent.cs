using EvenSystemCore;
using Enums;

namespace GameplayEvents
{
    public class UnlockFeatureEvent : EventBase
    {
        public EFeatureInGame featureInGame;

        public override void SetParameters(params object[] parameters)
        {
            featureInGame = (EFeatureInGame)parameters[0];
        }
    }
}