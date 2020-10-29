using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public CarInput input;
    public Rigidbody rbody;
    public Transform rayCaster;
    
    public float throttleSpeed;
    public float steerSpeed;

    public float maxAngle;

    public float finalSpeed;

    private RaycastHit hit;

    
    private Vector3 finalRotation;
    private Vector3 groundNormal;

    private bool grounded;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position = rbody.transform.position;
        float angle = transform.eulerAngles.y +
                      input.Steer * steerSpeed * Mathf.Sign(finalSpeed);

        finalRotation = (Quaternion.FromToRotation(transform.up, groundNormal) * transform.rotation).eulerAngles;
        finalRotation.y = angle;
        
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(finalRotation),
                1.5f*Time.deltaTime); //Quaternion.Euler(angle);

        finalSpeed = Mathf.SmoothStep(finalSpeed, (throttleSpeed * input.Throttle * Convert.ToInt16(grounded)),
            3 * Time.deltaTime);
        

    }

    private void FixedUpdate()
    {
        rbody.AddForce(finalSpeed * transform.forward, ForceMode.Acceleration);
        rbody.AddForce(Vector3.down * 50, ForceMode.Acceleration);
        grounded = false;
        Ray ray = new Ray(rayCaster.position, -Vector3.up);
        if (Physics.Raycast(ray, out hit, 2))
        {
            print(hit.transform.name);
            groundNormal = hit.normal;
            grounded = true;
        }
        

        //rbody.drag = grounded ? 3 : 0.1f;

    }
}
