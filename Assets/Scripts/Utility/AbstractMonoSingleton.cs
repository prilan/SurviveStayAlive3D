using System.Collections;
using UnityEngine;

namespace Utility
{
    public abstract class AbstractMonoSingleton<T> : MonoBehaviour where T : AbstractMonoSingleton<T>
    {
        private static T instance;

        public static bool Destroyed { get; private set; }

        public static T Instance
        {
            get
            {
                if (Destroyed) return null;

                if (instance != null) return instance;

                instance = FindObjectOfType(typeof(T)) as T;
                if (instance != null) return instance;

                GameObject gameObject = new GameObject(typeof(T).Name);
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
            Destroyed = true;
        }
    }
}