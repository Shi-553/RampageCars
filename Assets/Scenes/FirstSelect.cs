using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RampageCars
{
    public class FirstSelect : MonoBehaviour
    {
        void Start()
        {
            Select();
        }
        public void Select()
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }
    }
    }
