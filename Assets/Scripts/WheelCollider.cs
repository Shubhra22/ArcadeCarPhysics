
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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
            
        }

        private void DrawWheel(Vector3 wheelCenter)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, wheelCenter);
            Gizmos.DrawWireSphere(wheelCenter, 0.03f);
                
            Gizmos.color = Color.green;
            Gizmos.DrawLine(wheelCenter, wheelCenter - transform.up * radius );
                
            Handles.color = Color.red;
            Handles.DrawWireDisc(wheelCenter,transform.right,radius);
        }
    }
}

