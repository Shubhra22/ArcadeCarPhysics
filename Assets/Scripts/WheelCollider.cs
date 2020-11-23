
using System;
using System.Collections;
using System.Collections.Generic;
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
        
        // Start is called before the first frame update
        void Start()
        {
            rbody = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Ray ray = new Ray(transform.position, -transform.up);
            
            if (Physics.Raycast(ray, out hit, radius + suspensionLen, LayerMask.GetMask("Ground")))
            {
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
            GizmosExtension.DrawWireCircle(transform.position,transform.rotation,radius);
        }
    }
}

