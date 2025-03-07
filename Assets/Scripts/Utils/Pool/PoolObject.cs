using UnityEngine;

namespace Utils.Pool
{
    public class PoolObject : MonoBehaviour
    {
        private bool _isAvailable = false;

        public bool IsAvailable => _isAvailable;

        internal void CreatePoolObject() 
        {
            gameObject.SetActive(false);
            _isAvailable = true;
        }

        internal void ActivatePoolObject() 
        {
            gameObject.SetActive(true);
            _isAvailable = false;
        }

        internal void ReturnPoolObject() 
        {
            gameObject.SetActive(false);
            _isAvailable = true;
        }
    }
}