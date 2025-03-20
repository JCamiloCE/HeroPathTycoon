using Buildings;
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
        [SerializeField] private BuildingsManager _buildingsManager;

        private IRandom _random;
        private HeroDataScriptableObject _heroDataScriptableObject = null;
        private PoolControllerImpl<HeroController> _poolController = null;

        private void Awake()
        {
            _random = new RandomUnity();
            _heroDataScriptableObject = Resources.Load<HeroDataScriptableObject>("Scriptables/HerosDataScriptableObject");
            CreatePoolOfHeros();
        }

        public void SpawnNewHero()
        {
            HeroController newHero = _poolController.GetPoolObject();
            HeroData heroData = _heroDataScriptableObject.GetHeroDataByFamily(EHeroFamily.Candidate);

            if (!newHero.WasInitialized())
                newHero.Initialization(_mapManager.SelectHeroSpawnPoint(), _random, heroData, _buildingsManager);
            else
                newHero.SetNewHeroData(heroData);

            newHero.ActiveCurrentHero(_mapManager.GetPositionToWaitInLobby());
        }

        private void CreatePoolOfHeros() 
        {
            _poolController = new PoolControllerImpl<HeroController>();
            _poolController.SetPoolObject(_heroControllerBase, 5, true);
        }
    }
}