using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace RampageCars
{
    public class CameraReset : MonoBehaviour, ICamera
    {
        CinemachineFreeLook cinemaFreeLook;
        private int resetCount = 0;
        void Awake()
        {
            cinemaFreeLook = this.GetComponent<CinemachineFreeLook>();
            cinemaFreeLook.m_RecenterToTargetHeading.m_WaitTime = 0;
            cinemaFreeLook.m_RecenterToTargetHeading.m_RecenteringTime = 0.1f;
            cinemaFreeLook.m_YAxisRecentering.m_WaitTime = 0;
            cinemaFreeLook.m_YAxisRecentering.m_RecenteringTime = 0.1f;
        }

        void Update()
        {
            if (resetCount >= 0) { resetCount--; }
            else
            {
                cinemaFreeLook.m_RecenterToTargetHeading.m_enabled = false;
                cinemaFreeLook.m_YAxisRecentering.m_enabled = false;
            }
            
        }
        public void ViewReset()
        {
            cinemaFreeLook.m_RecenterToTargetHeading.m_enabled=true;
            cinemaFreeLook.m_YAxisRecentering.m_enabled = true;
            resetCount = 30;
        }
    }
}
