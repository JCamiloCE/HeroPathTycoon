using System.Collections.Generic;
using UnityEngine;
using Utils.Random;

namespace Map 
{
    public class MapManager : MonoBehaviour
    {
        [SerializeField] private Transform _positionForLobby = null;
        [SerializeField] private Transform _positionToWaitInLobby = null;
        [SerializeField] private Transform _positionForBarracks = null;
        [SerializeField] private List<Transform> _spawnPointsHero = null;

        private IRandom _random;

        private void Awake()
        {
            _random = new RandomUnity();
        }

        public Vector3 GetPositionForLobby() => _positionForLobby.position;
        public Vector3 GetPositionForBarracks() => _positionForBarracks.position;
        public Vector3 GetPositionToWaitInLobby() => _positionToWaitInLobby.position;

        public Vector3 SelectHeroSpawnPoint()
        {
            int random_index = _random.GetRandomIndexInList(_spawnPointsHero);
            return _spawnPointsHero[random_index].position;
        }
    }
}

