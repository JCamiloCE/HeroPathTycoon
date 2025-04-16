using System.Collections.Generic;
using UnityEngine;
using HeroPath.Scripts.Enums;

namespace Heros
{
    [CreateAssetMenu(fileName = "HerosData", menuName = "ScriptableObjects/HerosDataScriptableObject", order = 0)]
    public class HeroDataScriptableObject : ScriptableObject
    {
        public List<HeroData> herosDataScriptable;

        internal HeroData GetHeroDataByFamily(EHeroFamily heroFamily)
        {
            return herosDataScriptable.Find(x => x.GetHeroFamily == heroFamily);
        }
    }
}