using HeroPath.Scripts.Enums;
using JCC.Utils.GameplayEventSystem;
using HeroPath.Scripts.GameplayEvents;
using HeroPath.Scripts.GeneralManagers;
using System;
using UnityEngine;
using JCC.Utils.Random;
using JCC.Utils.LifeCycle;

namespace HeroPath.Scripts.Heros
{
    public class HeroController : MonoBehaviour, ILifeCycle
    {
        private HeroMovement _heroMovement = null;
        private HeroArt _heroArt = null;
        private HeroPath _heroPath = null;
        private float _heroSpeed = 1.0f;
        private bool _wasInitialzed = false;

        public bool WasInitialized() => _wasInitialzed;

        public bool Initialization(params object[] parameters)
        {
            MapManager mapManager = parameters[0] as MapManager;
            FeatureInGameManager featureInGameManager = parameters[1] as FeatureInGameManager;
            IRandom random = parameters[2] as IRandom;
            HeroData heroData = parameters[3] as HeroData;

            _heroSpeed = heroData.GetHeroSpeed;

            InitializeMovementComponent(random, mapManager.SelectHeroSpawnPoint());
            InitializeHeroArtComponent(heroData);
            InitializeHeroPath(mapManager, featureInGameManager, random);

            _wasInitialzed = true;
            return true;
        }

        public void MoveToNewPoint(Vector3 targetPosition, Action finishHeroMovement)
        {
            _heroMovement.GoToNewPosition(finishHeroMovement, targetPosition, _heroSpeed);
        }

        public void StartFadeOut(float time, bool overrideFade)
        {
            _heroArt.StartFadeOut(time, overrideFade);
        }

        public void StartFadeIn(float time, bool overrideFade)
        {
            _heroArt.StartFadeIn(time, overrideFade);
        }

        internal void EvolveHero(EHeroFamily heroFamily)
        {
            HeroDataScriptableObject heroDataScriptableObject = Resources.Load<HeroDataScriptableObject>("Scriptables/HerosDataScriptableObject");
            HeroData heroData = heroDataScriptableObject.GetHeroDataByFamily(heroFamily);
            _heroArt.EvolveHero(heroData.GetHeroSprite);
            _heroSpeed = heroData.GetHeroSpeed;
        }

        public void CallNextStepInHeroPath() 
        {
            _heroPath.IterateStep();
            Vector2 nextPosition = _heroPath.GetNextPosition();
            _heroMovement.GoToNewPosition(FinishMovement, nextPosition, _heroSpeed);
        }

        internal void SetNewHeroData(HeroData heroData)
        {
            _heroArt.SetNewHeroData(heroData.GetHeroSprite);
            _heroSpeed = heroData.GetHeroSpeed;
            _heroPath.CreateRandomPath();
            _heroMovement.SendToInitialPosition();
        }

        internal void ActiveCurrentHero() 
        {
            _heroArt.ActiveCurrentHero();
            CallNextStepInHeroPath();
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

        private void InitializeHeroPath(MapManager mapManager, FeatureInGameManager featureInGameManager, IRandom random) 
        {
            _heroPath = new();
            _heroPath.Initialization(mapManager, featureInGameManager, random);
            _heroPath.CreateRandomPath();
        }

        private void FinishMovement()
        {
            EBuildingType buildingType = _heroPath.GetTypeBuilding();
            if (buildingType == EBuildingType.None)
            {
                EventManager.TriggerEvent<FinishHeroPathEvent>(this);
            }
            else 
            {
                EventManager.TriggerEvent<StartProcessForHeroEvent>(this, buildingType);
            }
        }
    }
}