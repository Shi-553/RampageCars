using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RampageCars
{
    [RequireComponent(typeof(ICamera))]
    public class CameraController : MonoBehaviour
    {
        PlayerAction action;
        new ICamera camera;

        private void OnEnable()
        {
            action = new();
            action.Enable();
        }

        private void OnDisable()
        {
            action.Disable();
        }

        void Start()
        {
            var player = Singleton.Get<PlayerSingleton>().transform;

            var cinemachine = GetComponent<CinemachineFreeLook>();
            cinemachine.Follow = player;
            cinemachine.LookAt = player;

            camera = GetComponent<ICamera>();
            action.Camera.Reset.started += ResetStarted;
        }

        private void ResetStarted(InputAction.CallbackContext obj)
        {
            camera.ViewReset();
        }
    }
}
