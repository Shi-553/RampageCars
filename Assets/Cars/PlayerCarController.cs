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
            action.Player.Steer.performed += SteerPerformed;
            action.Player.Steer.canceled += SteerCanceled;
            action.Player.Boost.performed += BoostPerformed;

            car.SetMotorTorque(1);
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

        void Update()
        {
            var brake = action.Player.Brake.ReadValue<float>();
            if (brake > 0)
            {
                if (car.VelocityForward > 1)
                {
                    car.SetBrakeTorque(brake);
                    car.SetMotorTorque(0);
                }
                else
                {
                    car.SetBrakeTorque(0);
                    car.SetMotorTorque(-brake);
                }
            }
            else
            {
                if (car.VelocityForward < -1)
                {
                    car.SetBrakeTorque(1);
                    car.SetMotorTorque(0);
                }
                else
                {
                    car.SetBrakeTorque(0);
                    car.SetMotorTorque(1);
                }
            }
        }
    }
}
