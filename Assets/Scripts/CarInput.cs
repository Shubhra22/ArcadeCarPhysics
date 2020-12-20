using System;
using System.Collections;
using System.Collections.Generic;
using SocialNinja;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class CarInput : Manager<CarInput>
{
    public float Throttle { get; private set; }
    public float Steer { get; private set; }

    public float Brake { get; private set; }
    private CarInputAction _inputAction;
    // Start is called before the first frame update
    void Awake()
    {
        _inputAction = new CarInputAction();
    }

    private void OnEnable()
    {
        _inputAction.CarInput.Enable();
    }

    private void OnDisable()
    {
        _inputAction.CarInput.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        Throttle = _inputAction.CarInput.Throttle.ReadValue<float>();
        Steer = _inputAction.CarInput.Steer.ReadValue<float>();
        Brake = _inputAction.CarInput.Brake.ReadValue<float>();
    }
}
