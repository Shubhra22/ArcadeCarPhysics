
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
        public float radius;
        public float suspensionLen;
        public float suspensionStiffness;
        public float damping;
                
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
                Debug.DrawRay(transform.position,-transform.up * hit.distance,Color.blue);
                float suspensionForce = radius + suspensionLen - hit.distance;
                rbody.AddForce(transform.up * suspensionForce * suspensionStiffness);
               
            }
        }

        private void OnDrawGizmosSelected()
        {
            GizmosExtension.DrawWireCircle(transform.position,transform.rotation,radius);
        }
    }
}

