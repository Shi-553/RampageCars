using UnityEngine;

namespace RampageCars
{
    class PlayerActionManager : MonoBehaviour
    {

        DefaultPlayerAction defaultPlayerAction;
        IPlayerAction current;

        private void Awake()
        {
            defaultPlayerAction = GetComponent<DefaultPlayerAction>();
            current = defaultPlayerAction;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!current.CanChange)
            {
                current.OnCollisionEnter(collision);
                return;
            }
            defaultPlayerAction.OnCollisionEnter(collision);
        }

        public void DoAction<T>() where T : IPlayerAction
        {
            if (current.CanChange)
            {
                current = GetComponent<T>();
                current.Do();

                Debug.Log($"Do {typeof(T).Name}!");
            }
        }
        public void FinishAction<T>() where T : IPlayerAction
        {
            if (current.CanChange)
            {
                return;
            }

            IPlayerAction finidhed = GetComponent<T>();
            if (current == finidhed)
            {
                current.Finish();

                Debug.Log($"Finish {nameof(T)}!");
            }
        }
    }
}
