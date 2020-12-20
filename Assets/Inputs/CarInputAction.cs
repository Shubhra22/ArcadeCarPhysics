// GENERATED AUTOMATICALLY FROM 'Assets/Inputs/CarInputAction.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @CarInputAction : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @CarInputAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""CarInputAction"",
    ""maps"": [
        {
            ""name"": ""CarInput"",
            ""id"": ""4011485e-81ff-4fbe-9fb9-9a239c5e7258"",
            ""actions"": [
                {
                    ""name"": ""Steer"",
                    ""type"": ""Button"",
                    ""id"": ""1ee0a5f6-289e-4d17-8ee2-4b7c7acd5f4f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Throttle"",
                    ""type"": ""Button"",
                    ""id"": ""f330eb28-ac3d-49e1-89f9-dd6a336a8c7a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Brake"",
                    ""type"": ""Button"",
                    ""id"": ""a0ec0bf0-0828-4e17-89b9-61b299ae29f2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""ad719fa8-3db1-4911-bd2b-fdfac52b6366"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Steer"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""e2407641-d13d-4387-845a-1d0a3be1e526"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Steer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""7f3e5deb-4e02-4500-b4ce-fb410541ecd5"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Steer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""XBox"",
                    ""id"": ""51d92de3-8740-42dd-8d28-a02140a36254"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Steer"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""cd6d64b6-fe45-4230-964e-df6402ea72bd"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Steer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""bd26433d-889b-4409-848d-7220d0365c45"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Steer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""cbda95e7-77ed-40f8-8828-6b7a6e0a3f28"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Throttle"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""f6e4468c-53fc-41dc-b47e-3b91d30cc5db"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Throttle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""50f76015-1703-48ff-9863-5bac1edee1ba"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Throttle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""XBox"",
                    ""id"": ""0fda296f-0a4f-409a-9a9f-0fe97ff4141f"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Throttle"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""ac8c0349-3873-4194-aebd-909af201315a"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Throttle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""9af91e55-ab9e-485c-8a4b-516281d90531"",
                    ""path"": ""<AndroidGamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Throttle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""da339c08-d53b-460e-89eb-46b6fe253bde"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Brake"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ffed1ba5-f5db-4ecc-827c-7bfcbd4d0d41"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Brake"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // CarInput
        m_CarInput = asset.FindActionMap("CarInput", throwIfNotFound: true);
        m_CarInput_Steer = m_CarInput.FindAction("Steer", throwIfNotFound: true);
        m_CarInput_Throttle = m_CarInput.FindAction("Throttle", throwIfNotFound: true);
        m_CarInput_Brake = m_CarInput.FindAction("Brake", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // CarInput
    private readonly InputActionMap m_CarInput;
    private ICarInputActions m_CarInputActionsCallbackInterface;
    private readonly InputAction m_CarInput_Steer;
    private readonly InputAction m_CarInput_Throttle;
    private readonly InputAction m_CarInput_Brake;
    public struct CarInputActions
    {
        private @CarInputAction m_Wrapper;
        public CarInputActions(@CarInputAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Steer => m_Wrapper.m_CarInput_Steer;
        public InputAction @Throttle => m_Wrapper.m_CarInput_Throttle;
        public InputAction @Brake => m_Wrapper.m_CarInput_Brake;
        public InputActionMap Get() { return m_Wrapper.m_CarInput; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CarInputActions set) { return set.Get(); }
        public void SetCallbacks(ICarInputActions instance)
        {
            if (m_Wrapper.m_CarInputActionsCallbackInterface != null)
            {
                @Steer.started -= m_Wrapper.m_CarInputActionsCallbackInterface.OnSteer;
                @Steer.performed -= m_Wrapper.m_CarInputActionsCallbackInterface.OnSteer;
                @Steer.canceled -= m_Wrapper.m_CarInputActionsCallbackInterface.OnSteer;
                @Throttle.started -= m_Wrapper.m_CarInputActionsCallbackInterface.OnThrottle;
                @Throttle.performed -= m_Wrapper.m_CarInputActionsCallbackInterface.OnThrottle;
                @Throttle.canceled -= m_Wrapper.m_CarInputActionsCallbackInterface.OnThrottle;
                @Brake.started -= m_Wrapper.m_CarInputActionsCallbackInterface.OnBrake;
                @Brake.performed -= m_Wrapper.m_CarInputActionsCallbackInterface.OnBrake;
                @Brake.canceled -= m_Wrapper.m_CarInputActionsCallbackInterface.OnBrake;
            }
            m_Wrapper.m_CarInputActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Steer.started += instance.OnSteer;
                @Steer.performed += instance.OnSteer;
                @Steer.canceled += instance.OnSteer;
                @Throttle.started += instance.OnThrottle;
                @Throttle.performed += instance.OnThrottle;
                @Throttle.canceled += instance.OnThrottle;
                @Brake.started += instance.OnBrake;
                @Brake.performed += instance.OnBrake;
                @Brake.canceled += instance.OnBrake;
            }
        }
    }
    public CarInputActions @CarInput => new CarInputActions(this);
    public interface ICarInputActions
    {
        void OnSteer(InputAction.CallbackContext context);
        void OnThrottle(InputAction.CallbackContext context);
        void OnBrake(InputAction.CallbackContext context);
    }
}
