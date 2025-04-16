using HeroPath.Scripts.Enums;
using EvenSystemCore;

namespace HeroPath.Scripts.GameplayEvents
{
    public class UserCurrencyUIChangeEvent : EventBase
    {
        public ECurrency currencyChanged;
        public int newValue;

        public override void SetParameters(params object[] parameters)
        {
            currencyChanged = (ECurrency)parameters[0];
            newValue = (int)parameters[1];
        }
    }
}