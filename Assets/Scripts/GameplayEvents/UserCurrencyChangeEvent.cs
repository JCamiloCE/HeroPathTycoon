using HeroPath.Scripts.Enums;
using JCC.Utils.GameplayEventSystem;

namespace HeroPath.Scripts.GameplayEvents 
{
    public class UserCurrencyChangeEvent : EventBase
    {
        public ECurrency currencyChanged;
        public int delta;

        public override void SetParameters(params object[] parameters)
        {
            currencyChanged = (ECurrency)parameters[0];
            delta = (int)parameters[1];
        }
    }
}

