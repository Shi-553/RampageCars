using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RampageCars
{
    [RequireComponent(typeof(IPlayer))]
    [RequireComponent(typeof(SpeedMeasure))]
    public class PlayerController : MonoBehaviour
    {
        PlayerAction action;
        IPlayer car;

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
            car = GetComponent<IPlayer>();
            action.Player.Steer.performed += SteerPerformed;
            action.Player.Steer.canceled += SteerCanceled;
            action.Player.Boost.performed += BoostPerformed;
            action.Player.Boost.canceled += BoostCanceled;
            action.Player.Jump.started += JumpStarted; ;

            car.SetMotorTorque(1);
        }

        private void JumpStarted(InputAction.CallbackContext obj)
        {
            car.Jamp();
        }

        private void BoostCanceled(InputAction.CallbackContext obj)
        {
            car.Boost(Vector3.zero);
        }

        private void SteerCanceled(InputAction.CallbackContext obj)
        {
            car.SetSteerAngle(0);
        }

        private void BoostPerformed(InputAction.CallbackContext obj)
        {
            car.Boost(transform.forward);
        }

        private void SteerPerformed(InputAction.CallbackContext obj)
        {
            var steer = obj.ReadValue<float>();
            car.SetSteerAngle(steer);
        }

        void Update()
        {
            car.SetMotorTorque(1);
        }
    }
}
