
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
        
        
        
        // we need this two compressions to find the displacement (ds) of the spring.. we use the displacement to calculate the relative velocity. (hooks law)
        private float springCompression;
        private float lastSpringCompression;
        
        private RaycastHit hit;

        private bool isGrouned;
        
        
        
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
                
                // Apply static friction
                Vector3 velocityAtPoint = rbody.GetPointVelocity(hit.point);
                float sideWayFriction = (velocityAtPoint.x + transform.up.x) * suspensionForce;
                float forwardFriction = (velocityAtPoint.z + transform.up.z) * suspensionForce;
                float forwardDriveForce = throttleSpeed * suspensionForce;
                
                // we could get rid of the forward friction as well at this point...(ref. indie pixel). But I am not quite getting that idea/
                // I kept " -(transform.forward * forwardFriction)" this part 
                Vector3 resultantForce = transform.up * suspensionForce - transform.right * sideWayFriction +
                                         forwardDriveForce * transform.forward; //- transform.forward * forwardFriction;
                
                Debug.DrawLine(hit.point, resultantForce * 5);
                
                rbody.AddForceAtPosition(resultantForce,hit.point);
                isGrouned = true;
            }
            
            
        }

        private void Update()
        {
            
           transform.localRotation = Quaternion.Euler(Vector3.up * steerSpeed);
            WheelGraphicsPlacements();
            
        }

        void WheelGraphicsPlacements()
        {
            float springSize = suspensionLen - springCompression;
            Vector3 wheelCenter = transform.position - transform.up * springSize;
            wheelGraphics.transform.position = wheelCenter;
            wheelGraphics.transform.localRotation = transform.localRotation;
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

