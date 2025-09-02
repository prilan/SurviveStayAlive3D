using UnityEngine;

namespace Utility
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance != null) return instance;

                instance = FindObjectOfType(typeof(T)) as T;
                if (instance != null) return instance;

                var gameObject = new GameObject(typeof(T).Name);
                DontDestroyOnLoad(gameObject);

                instance = gameObject.AddComponent(typeof(T)) as T;

                return instance;
            }
        }

        public virtual void OnDestroy()
        {
            if (instance)
                Destroy(instance);

            instance = null;
        }
    }
}
