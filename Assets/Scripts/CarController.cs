﻿using System;
using UnityEngine;

namespace JoystickLab
{
    [Serializable]
    public struct Wheel
    {
        public string name;
        public WheelPhysics wheelCollider;
        public enum WheelType
        {
            ForwardWheel,
            RearWheel
        }
        public WheelType wheelType;
    }
    
    [RequireComponent(typeof(Rigidbody))]
    [ExecuteInEditMode]
    public class CarController : MonoBehaviour
    {
        private Rigidbody rbody;
        public Wheel[] wheels;
        
        private float throtle;
        private float steer;

        public float acceleration;
        public float steerAngle;

        public float accelRate; // How fast should it gain the max speed
    
        public CarInput Input;

        public Transform com;

        private float finalSpeed;
        private float finalTurnSpeed;

        [SerializeField] private bool useLerp;
        enum DriveType
        {
            ForwardWheelDrive,
            RearWheelDrive,
            FourWheelDrive
        }

        [SerializeField]private DriveType driveType;
        
        private bool grounded = true;
        // Start is called before the first frame update
        void Start()
        {
            rbody = GetComponent<Rigidbody>();
            rbody.centerOfMass = com.localPosition;
        }

        // Update is called once per frame
        void Update()
        {
            throtle = Input.Throttle;
            steer = Input.Steer;
            finalSpeed = useLerp
                ? Mathf.SmoothStep(finalSpeed, (throtle * acceleration),
                    accelRate * Time.deltaTime)
                : throtle * acceleration;
            
            DriveCar();
            //
            // if (Mathf.Abs(finalSpeed) > 0)
            // {
            //     finalTurnSpeed = Mathf.Lerp(finalTurnSpeed, steer * turnSpeed, 5 * Time.deltaTime);
            // }
        }

        void DriveCar()
        {
            switch (driveType)
            {
                case DriveType.ForwardWheelDrive:
                    foreach (Wheel wheel in wheels)
                    {
                        if (wheel.wheelType == Wheel.WheelType.ForwardWheel)
                        {
                            wheel.wheelCollider.throttleSpeed = finalSpeed;
                        }
                        else
                        {
                            wheel.wheelCollider.steerSpeed = steer * steerAngle;
                        }
                    }
                    break;
                case DriveType.RearWheelDrive:
                    foreach (Wheel wheel in wheels)
                    {
                        if (wheel.wheelType == Wheel.WheelType.RearWheel)
                        {
                            wheel.wheelCollider.throttleSpeed = finalSpeed;
                        }
                        else
                        {
                            wheel.wheelCollider.steerSpeed = steer * steerAngle;
                        }
                    }
                    break;
                case DriveType.FourWheelDrive:
                    foreach (Wheel wheel in wheels)
                    {
                        wheel.wheelCollider.throttleSpeed = finalSpeed;
                    }
                    break;
            }
        }
    }

}
