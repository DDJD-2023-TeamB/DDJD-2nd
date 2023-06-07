//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.0
//     from Assets/Player/PlayerInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""PlayerMovement"",
            ""id"": ""b55849ac-6bf4-4eba-bf4d-6393f601df07"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""e66faefa-6aed-49d8-821a-6bc7032ee1bc"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""5feedc10-40aa-4062-9e12-4ed185f72adf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Value"",
                    ""id"": ""2b3ee58e-e9dc-4bb5-9ebd-d95ffc70b09d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""d06f3f7d-7caa-4863-9d3f-43aa20b1faca"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""cafe9c71-1488-4ddc-8d4b-c00e01955368"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""2ceb461e-c0f4-400e-9ced-24605c5c11d8"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""91c4e42d-5f70-41a0-b7e0-06b79dc8190f"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""6c99fc65-0557-4efa-af42-d9e2537049e7"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""23728191-87ec-4874-b3ca-e3f1806ce8e1"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""610df77a-8731-4390-93ba-33aadb525425"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""d1e5a39e-2bd4-4714-9beb-a230cddb3979"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a290ae68-b47c-41c9-9257-d379c9f76e14"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c3ec16e3-9405-40ed-8fd7-1b116cca21a5"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9d71916b-dc38-42f7-97c3-5aec6962b94d"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""CameraControl"",
            ""id"": ""7ee2309a-92a3-4964-acf6-cb9466a833a0"",
            ""actions"": [
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""468e40d4-9718-403d-b281-252f53620725"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""0d3f78df-b04c-4ab3-995f-04b04b97396a"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Combat"",
            ""id"": ""3919ba5e-2838-45a3-9564-5bb380ec3c05"",
            ""actions"": [
                {
                    ""name"": ""Aim"",
                    ""type"": ""Button"",
                    ""id"": ""88863abb-6036-47c1-bb4f-f30dd3599809"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""CombineSpellShot"",
                    ""type"": ""Button"",
                    ""id"": ""5a98a77a-c208-4464-858e-6002567c5690"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LeftSpellShot"",
                    ""type"": ""Button"",
                    ""id"": ""0b274111-0b30-491e-808d-902da5d62f23"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RightSpellShot"",
                    ""type"": ""Button"",
                    ""id"": ""22769541-d336-4d03-a434-ebbda4a1d6d9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ChangeLeftSpell"",
                    ""type"": ""Button"",
                    ""id"": ""3cc3d25b-16eb-424d-8966-d144654bc646"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ChangeRightSpell"",
                    ""type"": ""Button"",
                    ""id"": ""c4ae74ac-747b-43cb-9f37-847b3f0d50ad"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MeleeAttack"",
                    ""type"": ""Button"",
                    ""id"": ""a84ce4f6-1563-467d-a2c6-0709eb840509"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""AbsorbMana"",
                    ""type"": ""Button"",
                    ""id"": ""f685dd20-e13e-444a-b7cb-466999df4b35"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""bd48e071-d5eb-46d6-9117-2833792a7719"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9e4916d9-830c-441c-b38d-cc01c5a6b9b9"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CombineSpellShot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3dfa945b-5681-4a0c-9743-a85c1f491175"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftSpellShot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""18f1e32c-719d-4247-b314-05e0a592de32"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightSpellShot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f803f591-f7ed-433c-b1c3-2be561026a5a"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeLeftSpell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b61deb5e-86c0-4496-8a44-1caa48cb3b6d"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeRightSpell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7e5aa81f-aa2d-468d-89f3-c33eb50f925e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MeleeAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e4e330f7-d4fb-4eb3-b0b6-0e5f2f26d651"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AbsorbMana"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""4c4086ac-2787-4baa-ac21-70c8a21b76e0"",
            ""actions"": [
                {
                    ""name"": ""Inventory"",
                    ""type"": ""Button"",
                    ""id"": ""81c358c4-0934-40b3-91d5-c01a5040de1a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Missions"",
                    ""type"": ""Button"",
                    ""id"": ""9f6e74ec-d688-47fa-9129-0ccf2b0f3236"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Menu"",
                    ""type"": ""Button"",
                    ""id"": ""bcebe0c0-8c69-4a02-9915-0009d735bb66"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""78dfa303-4190-417a-8cff-61ca1f6e496e"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3f04f4fe-1a67-4a11-a418-e34c9e427253"",
                    ""path"": ""<Keyboard>/m"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Missions"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6c157eb1-5793-47de-82db-72ddaa9a72a1"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerMovement
        m_PlayerMovement = asset.FindActionMap("PlayerMovement", throwIfNotFound: true);
        m_PlayerMovement_Move = m_PlayerMovement.FindAction("Move", throwIfNotFound: true);
        m_PlayerMovement_Run = m_PlayerMovement.FindAction("Run", throwIfNotFound: true);
        m_PlayerMovement_Jump = m_PlayerMovement.FindAction("Jump", throwIfNotFound: true);
        m_PlayerMovement_Dash = m_PlayerMovement.FindAction("Dash", throwIfNotFound: true);
        m_PlayerMovement_Interact = m_PlayerMovement.FindAction("Interact", throwIfNotFound: true);
        // CameraControl
        m_CameraControl = asset.FindActionMap("CameraControl", throwIfNotFound: true);
        m_CameraControl_Look = m_CameraControl.FindAction("Look", throwIfNotFound: true);
        // Combat
        m_Combat = asset.FindActionMap("Combat", throwIfNotFound: true);
        m_Combat_Aim = m_Combat.FindAction("Aim", throwIfNotFound: true);
        m_Combat_CombineSpellShot = m_Combat.FindAction("CombineSpellShot", throwIfNotFound: true);
        m_Combat_LeftSpellShot = m_Combat.FindAction("LeftSpellShot", throwIfNotFound: true);
        m_Combat_RightSpellShot = m_Combat.FindAction("RightSpellShot", throwIfNotFound: true);
        m_Combat_ChangeLeftSpell = m_Combat.FindAction("ChangeLeftSpell", throwIfNotFound: true);
        m_Combat_ChangeRightSpell = m_Combat.FindAction("ChangeRightSpell", throwIfNotFound: true);
        m_Combat_MeleeAttack = m_Combat.FindAction("MeleeAttack", throwIfNotFound: true);
        m_Combat_AbsorbMana = m_Combat.FindAction("AbsorbMana", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Inventory = m_UI.FindAction("Inventory", throwIfNotFound: true);
        m_UI_Missions = m_UI.FindAction("Missions", throwIfNotFound: true);
        m_UI_Menu = m_UI.FindAction("Menu", throwIfNotFound: true);
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

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // PlayerMovement
    private readonly InputActionMap m_PlayerMovement;
    private List<IPlayerMovementActions> m_PlayerMovementActionsCallbackInterfaces = new List<IPlayerMovementActions>();
    private readonly InputAction m_PlayerMovement_Move;
    private readonly InputAction m_PlayerMovement_Run;
    private readonly InputAction m_PlayerMovement_Jump;
    private readonly InputAction m_PlayerMovement_Dash;
    private readonly InputAction m_PlayerMovement_Interact;
    public struct PlayerMovementActions
    {
        private @PlayerInput m_Wrapper;
        public PlayerMovementActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PlayerMovement_Move;
        public InputAction @Run => m_Wrapper.m_PlayerMovement_Run;
        public InputAction @Jump => m_Wrapper.m_PlayerMovement_Jump;
        public InputAction @Dash => m_Wrapper.m_PlayerMovement_Dash;
        public InputAction @Interact => m_Wrapper.m_PlayerMovement_Interact;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMovementActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerMovementActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerMovementActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerMovementActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Run.started += instance.OnRun;
            @Run.performed += instance.OnRun;
            @Run.canceled += instance.OnRun;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @Dash.started += instance.OnDash;
            @Dash.performed += instance.OnDash;
            @Dash.canceled += instance.OnDash;
            @Interact.started += instance.OnInteract;
            @Interact.performed += instance.OnInteract;
            @Interact.canceled += instance.OnInteract;
        }

        private void UnregisterCallbacks(IPlayerMovementActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Run.started -= instance.OnRun;
            @Run.performed -= instance.OnRun;
            @Run.canceled -= instance.OnRun;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @Dash.started -= instance.OnDash;
            @Dash.performed -= instance.OnDash;
            @Dash.canceled -= instance.OnDash;
            @Interact.started -= instance.OnInteract;
            @Interact.performed -= instance.OnInteract;
            @Interact.canceled -= instance.OnInteract;
        }

        public void RemoveCallbacks(IPlayerMovementActions instance)
        {
            if (m_Wrapper.m_PlayerMovementActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerMovementActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerMovementActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerMovementActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerMovementActions @PlayerMovement => new PlayerMovementActions(this);

    // CameraControl
    private readonly InputActionMap m_CameraControl;
    private List<ICameraControlActions> m_CameraControlActionsCallbackInterfaces = new List<ICameraControlActions>();
    private readonly InputAction m_CameraControl_Look;
    public struct CameraControlActions
    {
        private @PlayerInput m_Wrapper;
        public CameraControlActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Look => m_Wrapper.m_CameraControl_Look;
        public InputActionMap Get() { return m_Wrapper.m_CameraControl; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraControlActions set) { return set.Get(); }
        public void AddCallbacks(ICameraControlActions instance)
        {
            if (instance == null || m_Wrapper.m_CameraControlActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_CameraControlActionsCallbackInterfaces.Add(instance);
            @Look.started += instance.OnLook;
            @Look.performed += instance.OnLook;
            @Look.canceled += instance.OnLook;
        }

        private void UnregisterCallbacks(ICameraControlActions instance)
        {
            @Look.started -= instance.OnLook;
            @Look.performed -= instance.OnLook;
            @Look.canceled -= instance.OnLook;
        }

        public void RemoveCallbacks(ICameraControlActions instance)
        {
            if (m_Wrapper.m_CameraControlActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ICameraControlActions instance)
        {
            foreach (var item in m_Wrapper.m_CameraControlActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_CameraControlActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public CameraControlActions @CameraControl => new CameraControlActions(this);

    // Combat
    private readonly InputActionMap m_Combat;
    private List<ICombatActions> m_CombatActionsCallbackInterfaces = new List<ICombatActions>();
    private readonly InputAction m_Combat_Aim;
    private readonly InputAction m_Combat_CombineSpellShot;
    private readonly InputAction m_Combat_LeftSpellShot;
    private readonly InputAction m_Combat_RightSpellShot;
    private readonly InputAction m_Combat_ChangeLeftSpell;
    private readonly InputAction m_Combat_ChangeRightSpell;
    private readonly InputAction m_Combat_MeleeAttack;
    private readonly InputAction m_Combat_AbsorbMana;
    public struct CombatActions
    {
        private @PlayerInput m_Wrapper;
        public CombatActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Aim => m_Wrapper.m_Combat_Aim;
        public InputAction @CombineSpellShot => m_Wrapper.m_Combat_CombineSpellShot;
        public InputAction @LeftSpellShot => m_Wrapper.m_Combat_LeftSpellShot;
        public InputAction @RightSpellShot => m_Wrapper.m_Combat_RightSpellShot;
        public InputAction @ChangeLeftSpell => m_Wrapper.m_Combat_ChangeLeftSpell;
        public InputAction @ChangeRightSpell => m_Wrapper.m_Combat_ChangeRightSpell;
        public InputAction @MeleeAttack => m_Wrapper.m_Combat_MeleeAttack;
        public InputAction @AbsorbMana => m_Wrapper.m_Combat_AbsorbMana;
        public InputActionMap Get() { return m_Wrapper.m_Combat; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CombatActions set) { return set.Get(); }
        public void AddCallbacks(ICombatActions instance)
        {
            if (instance == null || m_Wrapper.m_CombatActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_CombatActionsCallbackInterfaces.Add(instance);
            @Aim.started += instance.OnAim;
            @Aim.performed += instance.OnAim;
            @Aim.canceled += instance.OnAim;
            @CombineSpellShot.started += instance.OnCombineSpellShot;
            @CombineSpellShot.performed += instance.OnCombineSpellShot;
            @CombineSpellShot.canceled += instance.OnCombineSpellShot;
            @LeftSpellShot.started += instance.OnLeftSpellShot;
            @LeftSpellShot.performed += instance.OnLeftSpellShot;
            @LeftSpellShot.canceled += instance.OnLeftSpellShot;
            @RightSpellShot.started += instance.OnRightSpellShot;
            @RightSpellShot.performed += instance.OnRightSpellShot;
            @RightSpellShot.canceled += instance.OnRightSpellShot;
            @ChangeLeftSpell.started += instance.OnChangeLeftSpell;
            @ChangeLeftSpell.performed += instance.OnChangeLeftSpell;
            @ChangeLeftSpell.canceled += instance.OnChangeLeftSpell;
            @ChangeRightSpell.started += instance.OnChangeRightSpell;
            @ChangeRightSpell.performed += instance.OnChangeRightSpell;
            @ChangeRightSpell.canceled += instance.OnChangeRightSpell;
            @MeleeAttack.started += instance.OnMeleeAttack;
            @MeleeAttack.performed += instance.OnMeleeAttack;
            @MeleeAttack.canceled += instance.OnMeleeAttack;
            @AbsorbMana.started += instance.OnAbsorbMana;
            @AbsorbMana.performed += instance.OnAbsorbMana;
            @AbsorbMana.canceled += instance.OnAbsorbMana;
        }

        private void UnregisterCallbacks(ICombatActions instance)
        {
            @Aim.started -= instance.OnAim;
            @Aim.performed -= instance.OnAim;
            @Aim.canceled -= instance.OnAim;
            @CombineSpellShot.started -= instance.OnCombineSpellShot;
            @CombineSpellShot.performed -= instance.OnCombineSpellShot;
            @CombineSpellShot.canceled -= instance.OnCombineSpellShot;
            @LeftSpellShot.started -= instance.OnLeftSpellShot;
            @LeftSpellShot.performed -= instance.OnLeftSpellShot;
            @LeftSpellShot.canceled -= instance.OnLeftSpellShot;
            @RightSpellShot.started -= instance.OnRightSpellShot;
            @RightSpellShot.performed -= instance.OnRightSpellShot;
            @RightSpellShot.canceled -= instance.OnRightSpellShot;
            @ChangeLeftSpell.started -= instance.OnChangeLeftSpell;
            @ChangeLeftSpell.performed -= instance.OnChangeLeftSpell;
            @ChangeLeftSpell.canceled -= instance.OnChangeLeftSpell;
            @ChangeRightSpell.started -= instance.OnChangeRightSpell;
            @ChangeRightSpell.performed -= instance.OnChangeRightSpell;
            @ChangeRightSpell.canceled -= instance.OnChangeRightSpell;
            @MeleeAttack.started -= instance.OnMeleeAttack;
            @MeleeAttack.performed -= instance.OnMeleeAttack;
            @MeleeAttack.canceled -= instance.OnMeleeAttack;
            @AbsorbMana.started -= instance.OnAbsorbMana;
            @AbsorbMana.performed -= instance.OnAbsorbMana;
            @AbsorbMana.canceled -= instance.OnAbsorbMana;
        }

        public void RemoveCallbacks(ICombatActions instance)
        {
            if (m_Wrapper.m_CombatActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ICombatActions instance)
        {
            foreach (var item in m_Wrapper.m_CombatActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_CombatActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public CombatActions @Combat => new CombatActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private List<IUIActions> m_UIActionsCallbackInterfaces = new List<IUIActions>();
    private readonly InputAction m_UI_Inventory;
    private readonly InputAction m_UI_Missions;
    private readonly InputAction m_UI_Menu;
    public struct UIActions
    {
        private @PlayerInput m_Wrapper;
        public UIActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Inventory => m_Wrapper.m_UI_Inventory;
        public InputAction @Missions => m_Wrapper.m_UI_Missions;
        public InputAction @Menu => m_Wrapper.m_UI_Menu;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void AddCallbacks(IUIActions instance)
        {
            if (instance == null || m_Wrapper.m_UIActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_UIActionsCallbackInterfaces.Add(instance);
            @Inventory.started += instance.OnInventory;
            @Inventory.performed += instance.OnInventory;
            @Inventory.canceled += instance.OnInventory;
            @Missions.started += instance.OnMissions;
            @Missions.performed += instance.OnMissions;
            @Missions.canceled += instance.OnMissions;
            @Menu.started += instance.OnMenu;
            @Menu.performed += instance.OnMenu;
            @Menu.canceled += instance.OnMenu;
        }

        private void UnregisterCallbacks(IUIActions instance)
        {
            @Inventory.started -= instance.OnInventory;
            @Inventory.performed -= instance.OnInventory;
            @Inventory.canceled -= instance.OnInventory;
            @Missions.started -= instance.OnMissions;
            @Missions.performed -= instance.OnMissions;
            @Missions.canceled -= instance.OnMissions;
            @Menu.started -= instance.OnMenu;
            @Menu.performed -= instance.OnMenu;
            @Menu.canceled -= instance.OnMenu;
        }

        public void RemoveCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IUIActions instance)
        {
            foreach (var item in m_Wrapper.m_UIActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_UIActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public UIActions @UI => new UIActions(this);
    public interface IPlayerMovementActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
    public interface ICameraControlActions
    {
        void OnLook(InputAction.CallbackContext context);
    }
    public interface ICombatActions
    {
        void OnAim(InputAction.CallbackContext context);
        void OnCombineSpellShot(InputAction.CallbackContext context);
        void OnLeftSpellShot(InputAction.CallbackContext context);
        void OnRightSpellShot(InputAction.CallbackContext context);
        void OnChangeLeftSpell(InputAction.CallbackContext context);
        void OnChangeRightSpell(InputAction.CallbackContext context);
        void OnMeleeAttack(InputAction.CallbackContext context);
        void OnAbsorbMana(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnInventory(InputAction.CallbackContext context);
        void OnMissions(InputAction.CallbackContext context);
        void OnMenu(InputAction.CallbackContext context);
    }
}
