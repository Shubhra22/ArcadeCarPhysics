using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Suspension : MonoBehaviour
{
    private Rigidbody rbody;

    public float suspensionLength;
    public float suspensionForce;
    public Transform[] wheels;
    public Transform[] wheelTransforms;
    
    private float throtle;
    private float steer;

    public float acceleration;
    public float turnSpeed;
    
    public CarInput Input;

    public Transform com;
    
    private RaycastHit[] hit;

    private float finalSpeed;
    private float finalTurnSpeed;
    
    private bool grounded;
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        hit = new RaycastHit[wheelTransforms.Length];
    }

    // Update is called once per frame
    void Update()
    {
        throtle = Input.Throttle;
        steer = Input.Steer;

        finalSpeed = Mathf.SmoothStep(finalSpeed, (throtle * acceleration * Convert.ToInt16(grounded)),
            3 * Time.deltaTime);

        finalTurnSpeed = Mathf.Lerp(finalTurnSpeed, steer * turnSpeed, 10 * Time.deltaTime);

        
    }
    
    private void FixedUpdate()
    {
        grounded = false;
        //foreach (Transform w in wheels)
        for (int i = 0; i < wheels.Length; i++)
        {
            Transform w = wheels[i];
            Ray ray = new Ray(w.position,-w.up);
            if (Physics.Raycast(ray, out hit[i], suspensionLength,LayerMask.GetMask("Ground")))
            {
                grounded = true;
                if (hit[i].distance < suspensionLength)
                {
                    Vector3 force = Vector3.up * (9.8f * (suspensionLength - hit[i].distance) * suspensionForce) / 4;
                    rbody.AddForceAtPosition(force,w.position);
                }
            }
            rbody.AddForceAtPosition( -w.up * 10, w.position, ForceMode.Acceleration);
        }
        rbody.AddForceAtPosition( transform.forward * finalSpeed, transform.position, ForceMode.Acceleration);
        rbody.AddTorque(transform.up * finalTurnSpeed);
        if (!grounded)
        {
            rbody.AddForce(Vector3.down * 20, ForceMode.Acceleration);
        }
        
        for (int i = 0; i < hit.Length; i++)
        {
            wheelTransforms[i].position = hit[i].point + new Vector3(0,hit[i].distance,0);
            wheelTransforms[i].rotation = transform.rotation;
        }
    }
    
    void OnDrawGizmos() 
    {
        for (int i = 0; i < wheels.Length; i++)
        {
            Transform w = wheels[i];
            if (hit != null)
            {
                Handles.Label(w.position, hit[i].distance.ToString());
            }
        }
        
    }
}
