using System.Collections.Generic;
using Managers;

namespace CustomObjectPool
{
    public class PoolProvider : Singleton<PoolProvider>
    {
        private Dictionary<int, ObjectPool<BasePooledObject>> _objectPools;

        public override void Awake()
        {
            base.Awake();
            _objectPools = new Dictionary<int, ObjectPool<BasePooledObject>>();
        }

        public BasePooledObject GetPrefab(BasePooledObject prefab)
        {
            var prefabId = prefab.name.GetHashCode();
            if(_objectPools.TryGetValue(prefabId, out var pool))
            {
                var pooledObject = pool.Get();
                return pooledObject;
            }

            var newPool = new ObjectPool<BasePooledObject>(() =>
            {
                var pooledObject = Instantiate(prefab);
                pooledObject.name = prefab.name;
                pooledObject.PrefabId = prefabId;
                return pooledObject;
            });
            
            _objectPools.Add(prefabId, newPool);
            return newPool.Get();
        }

        public ObjectPool<BasePooledObject> GetPoolFromPrefabName(int prefabId)
        {
            return _objectPools.GetValueOrDefault(prefabId);
        }
    }
}