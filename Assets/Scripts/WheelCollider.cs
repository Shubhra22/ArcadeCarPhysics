
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

namespace JoystickLab
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Rigidbody))]
    public class WheelCollider : MonoBehaviour
    {
        [Range(0,5)]
        public float radius;// radius of my wheel
        public float suspensionLen; // How big the spring is
        public float stiffness; // kind of the force applied by the suspension spring (tightness of the spring). Differs from car to car
        public float damper; // Damper is used to slow down the force caused by the suspension spring... it kind of causes reverse force with the stiffness??
        
        // we need this two compressions to find the displacement (ds) of the spring.. we use the displacement to calculate the relative velocity. (hooks law)
        private float springCompression;
        private float lastSpringCompression;
        
        private RaycastHit hit;
        private Rigidbody rbody;

        private bool isGrouned;
        
        // Start is called before the first frame update
        void Start()
        {
            rbody = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Ray ray = new Ray(transform.position, -transform.up);
            isGrouned = false;
            if (Physics.Raycast(ray, out hit, radius + suspensionLen, LayerMask.GetMask("Ground")))
            {
                isGrouned = true;
                lastSpringCompression = springCompression;
                //Debug.DrawRay(transform.position,-transform.up * hit.distance,Color.blue);
                springCompression = radius + suspensionLen - hit.distance;
                float relativeVelocity = ( springCompression - lastSpringCompression) / Time.fixedDeltaTime;
                float suspensionForce = stiffness * springCompression + damper * relativeVelocity;
                rbody.AddForce(transform.up * suspensionForce);
            }
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
            Gizmos.DrawWireSphere(wheelCenter, 0.03f);
                
            Gizmos.color = Color.red;
            Gizmos.DrawLine(wheelCenter, wheelCenter - transform.up * radius );
                
            Handles.color = Color.red;
            Handles.DrawWireDisc(wheelCenter,transform.right,radius);
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

