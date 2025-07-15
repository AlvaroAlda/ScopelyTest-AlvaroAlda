using System.Collections.Generic;

namespace ObjectPool
{
    public class PoolProvider : Singleton<PoolProvider>
    {
        private Dictionary<string, ObjectPool<BasePooledObject>> _objectPools;

        public override void Awake()
        {
            base.Awake();
            _objectPools = new Dictionary<string, ObjectPool<BasePooledObject>>();
        }

        public BasePooledObject GetPrefab(BasePooledObject prefab)
        {
            if(_objectPools.TryGetValue(prefab.name, out var pool))
            {
                var pooledObject = pool.Get();
                pooledObject.gameObject.SetActive(true);
                return pooledObject;
            }

            var newPool = new ObjectPool<BasePooledObject>(() =>
            {
                var pooledObject = Instantiate(prefab);
                pooledObject.prefabName = prefab.name;
                return pooledObject;
            });
            
            _objectPools.Add(prefab.name, newPool);

            return newPool.Get();
        }

        public ObjectPool<BasePooledObject> GetPoolFromPrefabName(string prefabName)
        {
            return _objectPools.GetValueOrDefault(prefabName);
        }
    }
}