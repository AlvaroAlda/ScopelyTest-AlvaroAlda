using UnityEngine;

namespace Managers
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        public static T SharedInstance
        {
            get
            {
                if (_instance)
                    return _instance;
                
                var obj = new GameObject { name = typeof(T).Name };
                return obj.AddComponent<T>();
            }
        }

        public virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}