using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RampageCars
{
    [RequireComponent(typeof(ICamera))]
    public class CameraController : MonoBehaviour
    {
        PlayerAction action;
        ICamera camera;

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
            camera = GetComponent<ICamera>();
            action.Camera.Reset.started += ResetStarted;
        }

        private void ResetStarted(InputAction.CallbackContext obj)
        {
            camera.ViewReset();
        }
    }
}
