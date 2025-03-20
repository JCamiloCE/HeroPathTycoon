using Buildings;
using System;
using UnityEngine;
using Utils.Random;

namespace Heros
{
    public class HeroController : MonoBehaviour, ILifeCycle
    {
        private BuildingsManager _buildingsManager;
        private HeroMovement _heroMovement = null;
        private HeroArt _heroArt = null;
        private float _heroSpeed = 1.0f;
        private bool _wasInitialzed = false;

        public bool WasInitialized() => _wasInitialzed;

        public bool Initialization(params object[] parameters)
        {
            Vector3 initialPosition = (Vector3)parameters[0];
            IRandom random = parameters[1] as IRandom;
            HeroData heroData = parameters[2] as HeroData;
            _buildingsManager = parameters[3] as BuildingsManager;

            InitializeMovementComponent(random, initialPosition);
            InitializeHeroArtComponent(heroData);

            _wasInitialzed = true;
            return true;
        }

        public void SetNewHeroData(HeroData heroData)
        {
            _heroArt.SetNewHeroData(heroData.GetHeroSprite);
            _heroSpeed = heroData.GetHeroSpeed;
        }

        public void ActiveCurrentHero(Vector3 targetPosition) 
        {
            _heroArt.ActiveCurrentHero();
            _heroMovement.GoToNewPosition(FinishStartMovement, targetPosition, _heroSpeed);
        }

        public void EvolveHero(HeroData heroData) 
        {
            _heroArt.EvolveHero(heroData.GetHeroSprite);
        }

        public void MoveToNewPoint(Vector3 targetPosition, Action finishHeroMovement) 
        {
            _heroMovement.GoToNewPosition(finishHeroMovement, targetPosition, _heroSpeed);
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
            _heroArt.Initialization(heroData.GetHeroSprite);
        }

        private void FinishStartMovement()
        {
            _buildingsManager.AddHeroToBuilding(EBuildingType.Lobby, this);
        }

        //=================
        //=================
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                HeroDataScriptableObject heroDataScriptableObject = Resources.Load<HeroDataScriptableObject>("Scriptables/HerosDataScriptableObject");
                HeroData heroData = heroDataScriptableObject.GetHeroDataByFamily(EHeroFamily.Warrior);
                EvolveHero(heroData);
            }
        }
    }
}