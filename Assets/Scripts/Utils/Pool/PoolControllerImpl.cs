using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utils.Pool 
{ 
    public class PoolControllerImpl : IPoolController
    {
        private bool _expandPool = false;
        private List<PoolObject> _poolObjects = null;

        public int GetCurrentPoolSize() => _poolObjects.Count;

        public void SetPoolObject(GameObject initialPoolObject, int poolSize, bool expandPool) 
        {
            _expandPool = expandPool;
            _poolObjects = new List<PoolObject>();

            if (poolSize <= 0) 
                throw new ArgumentException("poolSize must be greater than zero", "poolSize");
            if (initialPoolObject == null)
                throw new ArgumentException("initialPoolObject is missing", "initialPoolObject");

            for (int i = 0; i < poolSize; i++)
            {
                _poolObjects.Add(CreateNewPoolObject(initialPoolObject));
            }
        }

        public PoolObject GetPoolObject() 
        {
            PoolObject poolObj = _poolObjects.Find(x => x.IsAvailable);

            if (poolObj == null && _expandPool) 
            {
                poolObj = CreateNewPoolObject(_poolObjects[0].gameObject);
                _poolObjects.Add(poolObj);
            }

            poolObj?.ActivatePoolObject();
            return poolObj;
        }

        public void ReturnToPool(PoolObject newPoolObj) 
        {
            if (newPoolObj == null)
                throw new ArgumentException("newPoolObj is Null", "newPoolObj");

            if(_poolObjects.Contains(newPoolObj))
                newPoolObj.ReturnPoolObject();
            else
                throw new ArgumentException("newPoolObj ", "newPoolObj");
        }

        private PoolObject CreateNewPoolObject(GameObject initialPoolObject) 
        {
            GameObject obj = GameObject.Instantiate(initialPoolObject);
            PoolObject newPoolObj = obj.AddComponent<PoolObject>();
            newPoolObj.CreatePoolObject();
            return newPoolObj;
        }
    }
}