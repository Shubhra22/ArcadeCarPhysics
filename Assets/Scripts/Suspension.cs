using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Suspension : MonoBehaviour
{
    public Transform[] wheels;
    public RaycastHit hit;
    public float suspensionLength;
    public float suspensionForce;

    public Rigidbody rbody;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        foreach (Transform w in wheels)
        {
            float yForce = rbody.mass * suspensionForce/4;
            //rbody.AddForce(Vector3.up * yForce);
            Ray ray = new Ray(w.position,-Vector3.up);
            if (Physics.Raycast(ray, out hit, suspensionLength,LayerMask.GetMask("Ground")))
            {
                Debug.DrawRay(w.position,-Vector3.up);
                 if(hit.distance < suspensionLength)
                     //rbody.AddForce(Vector3.up * yForce);
                     rbody.AddForce(Vector3.up * (1- hit.distance) * yForce);
            }
            
        }
    }
    
    void OnDrawGizmos() 
    {
        foreach (Transform w in wheels)
        {
            Handles.Label(w.position, hit.distance.ToString());
        }
        
    }
}
