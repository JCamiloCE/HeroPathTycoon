using UnityEngine;

namespace Utils.Pool
{
    public interface IPoolController
    {
        public void SetPoolObject(GameObject initialPoolObject, int poolSize, bool expandPool);
        public PoolObject GetPoolObject();
        public void ReturnToPool(PoolObject newPoolObj);
        public int GetCurrentPoolSize();
    }
}