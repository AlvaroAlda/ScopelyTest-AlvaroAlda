using System.Collections.Generic;

namespace ObjectPool
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
            if(_objectPools.TryGetValue(prefab.name.GetHashCode(), out var pool))
            {
                var pooledObject = pool.Get();
                pooledObject.gameObject.SetActive(true);
                return pooledObject;
            }

            var newPool = new ObjectPool<BasePooledObject>(() =>
            {
                var pooledObject = Instantiate(prefab);
                pooledObject.name = prefab.name;
                pooledObject.PrefabId = prefab.name.GetHashCode();
                return pooledObject;
            });
            
            _objectPools.Add(prefab.name.GetHashCode(), newPool);
            return newPool.Get();
        }

        public ObjectPool<BasePooledObject> GetPoolFromPrefabName(int prefabId)
        {
            return _objectPools.GetValueOrDefault(prefabId);
        }
    }
}