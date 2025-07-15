using System;
using UnityEngine;

namespace ObjectPool
{
    public abstract class BasePooledObject : MonoBehaviour
    {
        protected abstract void OnSpawn();
        protected abstract void OnDespawn();
        public string prefabName;

        protected virtual void OnEnable()
        {
            OnSpawn();
        }

        protected virtual void OnDisable()
        {
            PushToPool();
            OnDespawn();
        }

        private void PushToPool()
        {
            var pool = PoolProvider.SharedInstance.GetPoolFromPrefabName(prefabName);
            pool?.Push(this);
        }
    }
}