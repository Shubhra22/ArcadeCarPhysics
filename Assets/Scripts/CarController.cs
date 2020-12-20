using System;
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
        
        [Header("Wheel Configuration")]
        public Wheel[] wheels;
        public Transform com;
        enum DriveType
        {
            ForwardWheelDrive,
            RearWheelDrive,
            FourWheelDrive
        }
        [SerializeField] private DriveType driveType;

        enum SteerType
        {
            AckermanSteering,
            ParallelSteering,
        }
        [SerializeField] private SteerType steerType;
        
        private float _throtle;// store input throttle
        private float _steer; // store input steer
        private float _brake; // store input brake
        

        [Header("Car Speed and Steering")]
        public float acceleration;
        public float accelRate; // How fast should it gain the max speed

        [Tooltip("the radius of the turn Standard 10.4-10.7m")]
        public float steerRadius; // the radius of the turn as experienced by the centerline of the vehicle.
        [Tooltip("Distance between the two axles")]
        public float wheelBase; //the wheelbase of the vehicle (distance between the two axles).
        [Tooltip("Distance between center line of each tyre")]
        public float trackDist; // the track (distance between center line of each tyre).
        public float steerSpeedRate; //who fast the steer will change
        [SerializeField] private bool useLerp;
        
        [Header("Input Configuration")]
        public CarInput Input;
        
        private float finalSpeed;
        private float finalTurnSpeed;

        private bool grounded = true;
        // Start is called before the first frame update
        void Start()
        {
            rbody = GetComponent<Rigidbody>();
            rbody.centerOfMass = com.localPosition;
            foreach (Wheel wheel in wheels)
            {
                wheel.wheelCollider.SetSteerParam(wheelBase,steerRadius,trackDist,steerSpeedRate);
            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            _throtle = Input.Throttle;
            _steer = Input.Steer;
            finalSpeed = useLerp
                ? Mathf.SmoothStep(finalSpeed, (_throtle * acceleration),
                    accelRate * Time.deltaTime)
                : _throtle * acceleration;
            DriveCar();
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
                            wheel.wheelCollider.steerSpeed = _steer * steerRadius;
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
                            wheel.wheelCollider.steerSpeed = _steer * steerRadius;
                        }
                    }
                    break;
                case DriveType.FourWheelDrive:
                    foreach (Wheel wheel in wheels)
                    {
                        if (wheel.wheelType == Wheel.WheelType.ForwardWheel)
                        {
                            wheel.wheelCollider.steerSpeed = _steer * steerRadius;
                        }
                        wheel.wheelCollider.throttleSpeed = finalSpeed;
                    }
                    break;
            }
            
        }
    }

}
