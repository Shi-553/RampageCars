﻿using System.Collections;
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
            action.Player.Accel.performed += BoostPerformed;
            action.Player.Jump.started += JumpStarted;

            car.SetMotorTorque(1);
        }

        private void JumpStarted(InputAction.CallbackContext obj)
        {
            car.Jamp();
        }

        private void SteerCanceled(InputAction.CallbackContext obj)
        {
            car.SetSteerAngle(0);
        }

        private void BoostPerformed(InputAction.CallbackContext obj)
        {
            car.DoAction<AccelPlayerAction>();
        }

        private void SteerPerformed(InputAction.CallbackContext obj)
        {
            var steer = obj.ReadValue<float>();
            car.SetSteerAngle(steer);
        }

        void Update()
        {
            var motor = action.Player.Motor.ReadValue<float>();
            if (motor < 0)
            {
                car.SetMotorTorque(motor);
            }
            else
            {
                car.SetMotorTorque(1);
            }
        }
    }
}
