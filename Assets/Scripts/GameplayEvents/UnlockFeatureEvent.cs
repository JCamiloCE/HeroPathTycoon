using JCC.Utils.GameplayEventSystem;
using HeroPath.Scripts.Enums;

namespace HeroPath.Scripts.GameplayEvents
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