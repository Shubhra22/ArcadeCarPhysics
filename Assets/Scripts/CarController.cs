using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[ExecuteInEditMode]
public class CarController : MonoBehaviour
{
    private Rigidbody rbody;

    // public float suspensionLength;
    // public float suspensionForce;
    // public Transform[] wheels;
    // public Transform[] wheelTransforms;
    // public float wheelRadius;
    
    private float throtle;
    private float steer;

    public float acceleration;
    public float turnSpeed;
    
    public CarInput Input;

    public Transform com;
    
    //private RaycastHit[] hit;

    private float finalSpeed;
    private float finalTurnSpeed;
    
    
    
    private bool grounded = true;
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        rbody.centerOfMass = com.localPosition;//transform.localPosition - new Vector3(0, 0.1f, 0);
        //hit = new RaycastHit[wheelTransforms.Length];
    }

    // Update is called once per frame
    void Update()
    {
        // throtle = Input.Throttle;
        // steer = Input.Steer;
        //
        // finalSpeed = Mathf.SmoothStep(finalSpeed, (throtle * acceleration * Convert.ToInt16(grounded)),
        //     5 * Time.deltaTime);
        //
        // if (Mathf.Abs(finalSpeed) > 0)
        // {
        //     finalTurnSpeed = Mathf.Lerp(finalTurnSpeed, steer * turnSpeed, 5 * Time.deltaTime);
        // }
        
    }
    
    private void FixedUpdate()
    {
        //grounded = false;
        // for (int i = 0; i < wheels.Length; i++)
        // {
        //     Transform w = wheels[i];
        //     Ray ray = new Ray(w.position,-w.up);
        //     if (Physics.Raycast(ray, out hit[i], suspensionLength,LayerMask.GetMask("Ground")))
        //     {
        //         grounded = true;
        //         if (hit[i].distance < suspensionLength)
        //         {
        //             Vector3 force = Vector3.up * (9.8f * (suspensionLength - hit[i].distance) * suspensionForce) / 4;
        //             rbody.AddForceAtPosition(force,w.position);
        //         }
        //     }
        //     // add downward force to simulate gravity in each wheel
        //     rbody.AddForceAtPosition( -w.up * 10, w.position, ForceMode.Acceleration);
        //     wheelTransforms[i].position = w.position - new Vector3(0,(suspensionLength - hit[i].distance) - wheelRadius,0);
        // }
        
        
        //rbody.AddForceAtPosition( transform.forward * finalSpeed, transform.position, ForceMode.Acceleration);
        //rbody.AddTorque(transform.up * finalTurnSpeed);
    }
    
    // void OnDrawGizmos() 
    // {
    //     for (int i = 0; i < wheels.Length; i++)
    //     {
    //         Transform w = wheels[i];
    //         if (hit != null)
    //         {
    //             Handles.Label(w.position, hit[i].distance.ToString());
    //         }
    //     }
    //     
    // }
}
