using JCC.Utils.LifeCycle;
using System;
using System.Collections;
using UnityEngine;
using JCC.Utils.Random;

namespace HeroPath.Scripts.Heros
{
    public class HeroMovement : MonoBehaviour, ILifeCycle
    {
        private bool _wasInitialized = false;
        private Transform _heroTransform = null;
        private IRandom _random = null;
        private Action _finishMovement = null;
        private Coroutine _currentMovement = null;
        private Vector3 _initialPosition = Vector3.zero;

        public bool WasInitialized() => _wasInitialized;

        public bool Initialization(params object[] parameters)
        {
            if (_wasInitialized)  
                return false; 

            _random = parameters[0] as IRandom;

            _heroTransform = gameObject.transform;
            _wasInitialized = true;
            return true;
        }

        internal void SetInitialPosition(Vector3 initialPosition)
        {
            initialPosition.x = _random.GetRandomFloatBetween(initialPosition.x - 1f, initialPosition.x + 1f);
            _initialPosition = initialPosition;
            SendToInitialPosition();
        }

        internal void GoToNewPosition(Action finishMovement, Vector3 targetPosition, float moveSpeed) 
        {
            _finishMovement = finishMovement;
            StopCurrentMovement();
            _currentMovement = StartCoroutine(MoveObject(targetPosition, moveSpeed));
        }

        internal void SendToInitialPosition() 
        {
            _heroTransform.position = _initialPosition;
        }

        private void StopCurrentMovement()
        {
            if (_currentMovement != null)
            {
                StopCoroutine(_currentMovement);
                _currentMovement = null;
            }
        }

        private IEnumerator MoveObject(Vector3 targetPosition, float moveSpeed)
        {
            Vector3 startPosition = _heroTransform.position;
            float distance = Vector3.Distance(startPosition, targetPosition);
            float finishTime = distance/moveSpeed;
            float timeElapsed = 0f;

            while (timeElapsed < finishTime)
            {
                _heroTransform.position = Vector3.Lerp(startPosition, targetPosition, timeElapsed / finishTime);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            _finishMovement?.Invoke();
        }
    }
}