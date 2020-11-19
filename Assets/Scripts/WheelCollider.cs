using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelCollider : MonoBehaviour
{
        public float radius;
    // Start is called before the first frame update
    void Start()
    {
        
    }

        // Update is called once per frame
        void FixedUpdate()
        {
            
        }

        private void OnDrawGizmosSelected()
        {
            GizmosExtension.DrawWireCircle(transform.position,transform.rotation,radius);
        }
    }
}

