using HeroPath.Scripts.Enums;
using System;
using UnityEngine;

namespace HeroPath.Scripts.Heros
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
