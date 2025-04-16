using System;
using UnityEngine;
using HeroPath.Scripts.Enums;

namespace Heros
{
    [Serializable]
    public class HeroData
    {
        [SerializeField] private EHeroFamily _heroFamily;
        [SerializeField] private Sprite _heroSprite;
        [SerializeField] private float _heroSpeed;

        public EHeroFamily GetHeroFamily => _heroFamily;
        public Sprite GetHeroSprite => _heroSprite;
        public float GetHeroSpeed => _heroSpeed;
    }

}
