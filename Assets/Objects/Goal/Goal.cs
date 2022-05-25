using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{

    public class Goal : MonoBehaviour
    {
        [SerializeField]
        SceneType clearScene;
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<IPlayerTag>() != null)
            {
                Singleton.Get<GameRoleManager>().GameClear();
            }
        }
    }
}
