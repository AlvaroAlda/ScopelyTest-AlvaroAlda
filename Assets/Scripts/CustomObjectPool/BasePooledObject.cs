using UnityEngine;

namespace CustomObjectPool
{
    public abstract class BasePooledObject : MonoBehaviour
    {
        protected abstract void OnSpawn();
        protected abstract void OnDespawn();
        
        public int PrefabId { get; set; }
        private static bool _isQuitting;

        protected virtual void OnEnable()
        {
            OnSpawn();
        }

        protected virtual void OnDisable()
        {
            PushToPool();
            OnDespawn();
        }
        
        private void OnApplicationQuit()
        {
            _isQuitting = true;
        }

        private void PushToPool()
        {
            if (_isQuitting)
                return;
            
            var pool = PoolProvider.SharedInstance.GetPoolFromPrefabName(PrefabId);
            pool?.Push(this);
        }
    }
}