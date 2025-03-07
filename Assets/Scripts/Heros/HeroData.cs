using System;
using UnityEngine;

namespace Heros
{
    [Serializable]
    public class HeroData
    {
        [SerializeField] private EHeroFamily _heroFamily;
        [SerializeField] private Sprite _heroSprite;

        public EHeroFamily GetHeroFamily => _heroFamily;
        public Sprite GetHeroSprite => _heroSprite;
    }

}
