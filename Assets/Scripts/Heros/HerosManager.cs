using Map;
using UnityEngine;
using Utils.Pool;
using Utils.Random;

namespace Heros
{
    public class HerosManager : MonoBehaviour
    {
        [SerializeField] private GameObject _heroControllerBase;
        [SerializeField] private MapManager _mapManager;

        private IRandom _random;
        private HeroDataScriptableObject _heroDataScriptableObject = null;
        private PoolControllerImpl _poolController = null;

        private void Awake()
        {
            _random = new RandomUnity();
            _heroDataScriptableObject = Resources.Load<HeroDataScriptableObject>("Scriptables/HerosDataScriptableObject");
            CreatePoolOfHeros();
        }

        public void SpawnNewHero()
        {
            HeroController newHero = _poolController.GetPoolObject().GetComponent<HeroController>();

            if (!newHero.WasInitialized())
            {
                HeroData heroData = _heroDataScriptableObject.GetHeroDataByFamily(EHeroFamily.Candidate);
                newHero.Initialization(_mapManager.SelectHeroSpawnPoint(), _random, heroData);
            }

            newHero.ActiveCurrentHero();
        }

        private void CreatePoolOfHeros() 
        {
            _poolController = new PoolControllerImpl();
            _poolController.SetPoolObject(_heroControllerBase, 5, true);
        }
    }
}