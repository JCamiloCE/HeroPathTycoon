using UnityEngine;
using Utils.Random;

namespace Heros
{
    public class HeroMovement : MonoBehaviour, ILifeCycle
    {
        private bool _wasInitialized = false;
        private Transform _heroTransform = null;
        private IRandom _random;

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
            _heroTransform.position = initialPosition;
        }
    }
}