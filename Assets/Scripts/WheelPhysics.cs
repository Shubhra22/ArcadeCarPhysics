
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

namespace JoystickLab
{
    [ExecuteInEditMode]
    public class WheelPhysics : MonoBehaviour
    {
        [SerializeField] private Transform wheelGraphics;
        [SerializeField] private Rigidbody rbody;
        [SerializeField] [Range(0,5)] private float radius;// radius of my wheel
        [SerializeField] private float suspensionLen; // How big the spring is
        [SerializeField] private float stiffness; // kind of the force applied by the suspension spring (tightness of the spring). Differs from car to car
        [SerializeField] private float damper; // Damper is used to slow down the force caused by the suspension spring... it kind of causes reverse force with the stiffness??

        public float throttleSpeed;
        public float steerSpeed;

        private Vector3 _wheelVelocity;
        
        // we need this two compressions to find the displacement (ds) of the spring.. we use the displacement to calculate the relative velocity. (hooks law)
        private float springCompression;
        private float lastSpringCompression;
        
        private RaycastHit hit;

        private bool isGrouned;

        private float finalTurnSpeed;
        
        // L is the wheelbase of the vehicle (distance between the two axles).
        // T is the track (distance between center line of each tyre).
        // R is the radius of the turn as experienced by the centerline of the vehicle.
        private float _wheelBase = 2.2f;
        private float _track = 6;
        private float _turnRadius = 10;
        private float _turnSpeedRate = 5;
        
        private enum WheelPos
        {
            Left,
            Right
        }

        [SerializeField]private WheelPos wheelPos;
        // Start is called before the first frame update
        void Start()
        {
            //rbody = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Ray ray = new Ray(transform.position, -transform.up);
            isGrouned = false;
            if (Physics.Raycast(ray, out hit, radius + suspensionLen, LayerMask.GetMask("Ground")))
            {
                
                lastSpringCompression = springCompression;
                springCompression = radius + suspensionLen - hit.distance;
                float relativeVelocity = ( springCompression - lastSpringCompression) / Time.fixedDeltaTime;
                float suspensionForce = stiffness * springCompression + damper * relativeVelocity;

                _wheelVelocity = transform.InverseTransformDirection(rbody.GetPointVelocity(hit.point));
               
                // Apply static friction
                float sideWayFriction = (_wheelVelocity.x + transform.up.x) * suspensionForce;
                float forwardFriction = (_wheelVelocity.z + transform.up.z) * suspensionForce/5;
                float forwardDriveForce = throttleSpeed * suspensionForce;

                Vector3 up = transform.up;
                up.z = 0;
                // we could get rid of the forward friction as well at this point...(ref. indie pixel). But I am not quite getting that idea/
                // I kept " -(transform.forward * forwardFriction)" this part 
                Vector3 resultantForce = up * suspensionForce - transform.right * sideWayFriction +
                                         forwardDriveForce * transform.forward;// - transform.forward * forwardFriction;
                
                rbody.AddForceAtPosition(resultantForce,hit.point);
                isGrouned = true;
            }
            
            
        }

        private void Update()
        {
            Steer(_wheelBase,_turnRadius,_track);
            WheelGraphicsPlacements();
            
        }

        public void SetSteerParam(float L, float R, float T, float turnRate)
        {
            _track = T;
            _turnRadius = R;
            _wheelBase = L;
            _turnSpeedRate = turnRate;
        }

        // Acerman Steering https://datagenetics.com/blog/december12016/index.html
        // L is the wheelbase of the vehicle (distance between the two axles).
        // T is the track (distance between center line of each tyre).
        // R is the radius of the turn as experienced by the centerline of the vehicle.
        void Steer(float L, float R, float T)
        {
            float insideWheelAngle = Mathf.Rad2Deg * Mathf.Atan2(L , (R - T / 2));
            float outsideWheelAngle = Mathf.Rad2Deg * Mathf.Atan2(L , (R + T / 2));
            
            switch (wheelPos)
            {
                case WheelPos.Left:
                    finalTurnSpeed = Mathf.Lerp(finalTurnSpeed, insideWheelAngle * Sign(steerSpeed), _turnSpeedRate * Time.deltaTime);
                    break;
                case WheelPos.Right:
                    finalTurnSpeed = Mathf.Lerp(finalTurnSpeed, outsideWheelAngle * Sign(steerSpeed), _turnSpeedRate * Time.deltaTime);
                    break;
            }
            transform.localRotation = Quaternion.Euler(Vector3.up * finalTurnSpeed);
        }
        
        int Sign(float number) {
            return number < 0 ? -1 : (number > 0 ? 1 : 0);
        }
        void WheelGraphicsPlacements()
        {
            float springSize = suspensionLen - springCompression;
            Vector3 wheelCenter = transform.position - transform.up * springSize;
            wheelGraphics.transform.position = wheelCenter;


            float rotationDir = Vector3.Dot(_wheelVelocity.normalized, transform.forward);
            // Calculate Angular Velocity... w = 2*pi*f from HighSchool physics.
            float circumference = 2f * Mathf.PI * radius; // d = 2*pi*r
            float frequency = _wheelVelocity.magnitude/circumference;// f = 1/t ; but d = vt, so f = 1/d/v ; => f=v/d
            float angularVelocity = 360 * frequency * rotationDir; // so angular velocity w = 2*pi*f. convert it to degree 2*pi = 360 

            wheelGraphics.transform.Rotate(angularVelocity * Time.deltaTime,0,0);
        }

        private void OnDrawGizmosSelected()
        {
#if UNITY_EDITOR
            float springSize = suspensionLen;
            Vector3 wheelCenter = Vector3.zero;
            
            
            if (Application.isPlaying && isGrouned)
            {
                springSize = (suspensionLen - springCompression);
                wheelCenter = transform.position - transform.up * springSize;
            }
            else
            {
                wheelCenter = transform.position - transform.up * springSize;
            }
            DrawWheel(wheelCenter);
            DrawSpring(springSize);
#endif
        }

        private void DrawWheel(Vector3 wheelCenter)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, wheelCenter);
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(wheelCenter, 0.01f);
            
            Gizmos.color = Color.red;
            Gizmos.DrawLine(wheelCenter, wheelCenter - transform.up * radius );
            Handles.color = Color.red;
            Handles.DrawWireDisc(wheelCenter,transform.right,radius);
            
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(wheelCenter - transform.up * radius, 0.01f);
        }

        private void DrawSpring(float springSize)
        {
            Handles.color = Color.green;
            int point_detail = 200;// more points = more refined curve
            Vector3[] points = new Vector3[point_detail];
            float pointdist = springSize / point_detail;
            float oscillation = 80;
            float r = 0.1f;
            float y = 0;
            
            for (int i = 0; i < point_detail; i++)
            {
                y += pointdist;
                float x = r * Mathf.Cos(y * oscillation/springSize);
                float z = r * Mathf.Sin(y * oscillation/springSize);
                points[i] = transform.position - new Vector3(x,y,z);
            }
            Handles.DrawAAPolyLine(points);
        }
    }
}

