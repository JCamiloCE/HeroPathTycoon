using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeneralManagers
{
    public class FeatureInGameManager : MonoBehaviour
    {
        private Dictionary<EFeatureInGame, bool> _featureInGame = new ();

        public bool IsFeatureUnlock(EFeatureInGame feature) 
        {
            if (_featureInGame.ContainsKey(feature)) 
            {
                return _featureInGame[feature];
            }

            Debug.LogError("Feature not found: " + feature);
            return false;
        }
        
        private void Awake()
        {
            InitializeFeatures();
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