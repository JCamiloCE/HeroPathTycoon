using Heros;
using Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Buildings
{
    public class BuildingHeroProcessor : MonoBehaviour, ILifeCycle
    {
        private List<HeroController> _heroControllers = null;
        private HeroController _currentHero = null;
        private Coroutine _moveQueueCoroutine = null;
        private Coroutine _heroProcessorCoroutine = null;
        private MapManager _mapManager = null;
        private BuildingArt _buildingArt = null;
        private float _timeToProcess = 1;
        private bool _wasInitialized = false;

        public bool Initialization(params object[] parameters)
        {
            _mapManager = parameters[0] as MapManager;
            _buildingArt = parameters[1] as BuildingArt;
            _timeToProcess = (float)parameters[2];
            _heroControllers = new ();
            _wasInitialized = true;
            return _wasInitialized;
        }

        public bool WasInitialized() => _wasInitialized;

        internal void AddHeroToQueue(HeroController heroController)
        {
            _heroControllers.Add(heroController);
            RunMoveQueue();
        }

        private void RunMoveQueue() 
        {
            if (_moveQueueCoroutine != null)
            {
                StopCoroutine(_moveQueueCoroutine);
                _moveQueueCoroutine = null;
            }

            SetNewPositionForQueue(_currentHero != null);
        }

        private void SetNewPositionForQueue(bool isProcessHero) 
        {
            for (int i = 0; i < _heroControllers.Count; i++)
            {
                if (isProcessHero)
                    _heroControllers[i].MoveToNewPoint(GetPositionByIndex(i + 1), null);
                else
                {
                    if (i == 0)
                    {
                        _heroControllers[i].MoveToNewPoint(GetPositionByIndex(i), OnFinishHeroMovement);
                        _heroControllers[i].StartFadeOut(1f, overrideFade:false);
                    }
                    else 
                    {
                        _heroControllers[i].MoveToNewPoint(GetPositionByIndex(i), null);
                    }
                }
            }
        }

        private Vector3 GetPositionByIndex(int index) 
        {
            Vector3 newPosition = _mapManager.GetPositionToStartQueue();
            newPosition.y -= (index * 2);
            return newPosition;
        }

        private void OnFinishHeroMovement() 
        {
            _currentHero = _heroControllers[0];
            _heroControllers.RemoveAt(0);
            RunProcessor();
        }

        private void RunProcessor()
        {
            if (_heroProcessorCoroutine == null)
            {
                _heroProcessorCoroutine = StartCoroutine(ProcessHero());
            }
            else 
            {
                Debug.LogError("BuildingHeroProcessor.TryRunProcessor: try to process two heros at the same time");
            }
        }

        private IEnumerator ProcessHero() 
        {
            while(_currentHero != null) 
            {
                _buildingArt.StartProcess(_timeToProcess);
                yield return new WaitForSeconds(_timeToProcess);
                _currentHero.StartFadeIn(1f, overrideFade:true);
                _currentHero.MoveToNewPoint(new Vector3(0f,10f), null); //temp
                _currentHero = null;
            }
            Debug.Log("Finish hero process");
            RunMoveQueue();
            _heroProcessorCoroutine = null;
        }
    }
}