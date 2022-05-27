using System.Collections.Generic;
using EditorScripts;
using UnityEngine;

namespace RampageCars
{
    class PlayerActionManager : MonoBehaviour
    {

        DefaultPlayerAction defaultPlayerAction;
        IPlayerAction current;
        IPlayerAction EnabledCurrent => current.CanChange ? defaultPlayerAction : current;

        IPlayerAction[] actions;

        private void Awake()
        {
            defaultPlayerAction = GetComponent<DefaultPlayerAction>();
            current = defaultPlayerAction;

            actions=GetComponentsInChildren<IPlayerAction>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            EnabledCurrent.CollisionEnter(collision);
        }
        private void OnCollisionStay(Collision collision)
        {
            EnabledCurrent.CollisionStay(collision);
        }
        private void OnCollisionExit(Collision collision)
        {
            EnabledCurrent.CollisionExit(collision);
        }

        IPlayerAction GetAction<T>()
        {
            foreach (var action in actions)
            {
                if (action is T)
                {
                    return action;
                }
            }
            return defaultPlayerAction;
        }
        public void DoAction<T>() where T : IPlayerAction
        {
            if (!current.CanChange)
            {
                current.Finish();
                Debug.Log($"Finish {current.GetType().Name}!");
            }

            current = GetAction<T>();
            current.Do();

            Debug.Log($"Do {typeof(T).Name}!");
        }
        public void FinishAction<T>() where T : IPlayerAction
        {
            if (current.CanChange)
            {
                return;
            }

            IPlayerAction finidhed = GetAction<T>();
            if (current == finidhed)
            {
                current.Finish();

                Debug.Log($"Finish {typeof(T).Name}!");
            }
        }
    }
}
