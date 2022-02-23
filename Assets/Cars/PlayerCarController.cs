using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RampageCars
{
    [RequireComponent(typeof(Car))]
    public class PlayerCarController : MonoBehaviour
    {
        PlayerAction action;

        Car car;

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
            car = GetComponent<Car>();

            action.Player.Brake.performed += BrakePerformed;
            action.Player.Brake.canceled += BrakeCanceled; ;
            action.Player.Steer.performed += SteerPerformed;
            action.Player.Steer.canceled += SteerCanceled;
            action.Player.Boost.performed += BoostPerformed;

            car.SetMotorTorque(1);
        }

        private void BrakeCanceled(InputAction.CallbackContext obj)
        {
            car.SetBrakeTorque(0);
            car.SetMotorTorque(1);
        }

        private void BrakePerformed(InputAction.CallbackContext obj)
        {
            var brake = obj.ReadValue<float>();
            car.SetBrakeTorque(brake);
            car.SetMotorTorque(0);
        }

        private void SteerCanceled(InputAction.CallbackContext obj)
        {
            car.SetSteerAngle(0);
        }

        private void BoostPerformed(InputAction.CallbackContext obj)
        {
        }

        private void SteerPerformed(InputAction.CallbackContext obj)
        {
            var steer = obj.ReadValue<float>();
            car.SetSteerAngle(steer);
        }

        // Update is called once per frame
        void Update()
        {

            }
    }
}
