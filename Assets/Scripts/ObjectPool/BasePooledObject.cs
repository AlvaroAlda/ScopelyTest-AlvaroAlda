using System;
using UnityEngine;

namespace ObjectPool
{
    public abstract class BasePooledObject : MonoBehaviour
    {
        protected abstract void OnSpawn();
        protected abstract void OnDespawn();
        
        public string PrefabName { get; set; }
        private static bool _isQuitting = false;

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
            
            var pool = PoolProvider.SharedInstance.GetPoolFromPrefabName(PrefabName);
            pool?.Push(this);
        }
    }
}