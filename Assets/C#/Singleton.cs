using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RampageCars
{
    public abstract class SingletonBase : MonoBehaviour
    {
        protected void Awake()
        {
            Singleton.Add(this);
            Init();
        }

        protected virtual void Init() { }
    }

    public static class Singleton
    {
        private static List<SingletonBase> instances = new();

        public static void Add(SingletonBase singleton)
        {
#if UNITY_EDITOR
            var type = singleton.GetType();
            foreach (var instance in instances)
            {
                if (instance.GetType() == type)
                {
                    Debug.LogError(type.Name + " is awlady exist!", singleton);
                    return;
                }
            }
#endif
            instances.Add(singleton);
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void SingletonSceneInitialize()
        {
            string managerSceneName = "Manager";
            if (!SceneManager.GetSceneByName(managerSceneName).IsValid())
            {
                SceneManager.LoadScene(managerSceneName, LoadSceneMode.Additive);
            }
        }


        public static T Get<T>() where T : SingletonBase
        {
            foreach (var singleton in instances)
            {
                if (singleton is T subclass)
                {
                    return subclass;
                }
            }
            Debug.LogError($"{ typeof(T).Name } is not found!");

            return GameObject.FindObjectOfType<T>();
        }

    }
}
