using JCC.Utils.GameplayEventSystem;
using HeroPath.Scripts.Enums;
using HeroPath.Scripts.GameplayEvents;
using System.Collections.Generic;
using UnityEngine;
using JCC.Utils.DebugManager;

namespace HeroPath.Scripts.GeneralManagers
{
    public class FeatureInGameManager : MonoBehaviour, IEventListener<BuildingPurchasedEvent>
    {
        private Dictionary<EFeatureInGame, bool> _featureInGame = new ();

        public bool IsFeatureUnlock(EFeatureInGame feature) 
        {
            if (_featureInGame.ContainsKey(feature)) 
            {
                return _featureInGame[feature];
            }

            DebugManager.LogError("Feature not found: " + feature);
            return false;
        }

        void IEventListener<BuildingPurchasedEvent>.OnEvent(BuildingPurchasedEvent event_data)
        {
            switch (event_data.buildingType)
            {
                case EBuildingType.Archery:
                    _featureInGame[EFeatureInGame.FeatureBuildingArcher] = true;
                    EventManager.TriggerEvent<UnlockFeatureEvent>(EFeatureInGame.FeatureBuildingArcher);
                    break;
            }
        }

        private void Awake()
        {
            InitializeFeatures();
            DebugManager.Initialization(new DebugUnityImpl(), EDebugScope.All);
            EventManager.AddListener(this);
        }

        private void InitializeFeatures() 
        {
            //Right now this will be write but in the future this will be change
            _featureInGame.Add(EFeatureInGame.FeatureBuildingBarracks, true);
            _featureInGame.Add(EFeatureInGame.FeatureBuildingLobby, true);
            _featureInGame.Add(EFeatureInGame.FeatureBuildingArcher, false);
        }
    }
}