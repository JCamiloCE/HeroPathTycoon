using UnityEngine;
using Utils.Random;

namespace Heros
{
    public class HeroController : MonoBehaviour, ILifeCycle
    {
        private HeroMovement _heroMovement = null;
        private HeroArt _heroArt = null;
        private bool _wasInitialzed = false;

        public bool WasInitialized() => _wasInitialzed;

        public bool Initialization(params object[] parameters)
        {
            Vector3 initialPosition = (Vector3)parameters[0];
            IRandom random = parameters[1] as IRandom;
            HeroData heroData = parameters[2] as HeroData;

            InitializeMovementComponent(random, initialPosition);
            InitializeHeroArtComponent(heroData);

            _wasInitialzed = true;
            return true;
        }

        public void ActiveCurrentHero() 
        {
            _heroArt.ActiveCurrentHero();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                HeroDataScriptableObject heroDataScriptableObject = Resources.Load<HeroDataScriptableObject>("Scriptables/HerosDataScriptableObject");
                HeroData heroData = heroDataScriptableObject.GetHeroDataByFamily(EHeroFamily.Warrior);
                EvolveHero(heroData);
            }
        }

        public void EvolveHero(HeroData heroData) 
        {
            _heroArt.EvolveHero(heroData);
        }

        private void InitializeMovementComponent(IRandom random, Vector3 initialPosition) 
        {
            _heroMovement = gameObject.GetComponent<HeroMovement>();
            _heroMovement.Initialization(random);
            _heroMovement.SetInitialPosition(initialPosition);
        }

        private void InitializeHeroArtComponent(HeroData heroData) 
        {
            _heroArt = gameObject.GetComponent<HeroArt>();
            _heroArt.Initialization(heroData);
        }
    }
}