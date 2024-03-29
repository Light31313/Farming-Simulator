//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/FarmingGame/Input/InputMaster.inputactions
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

public partial class @InputMaster : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""36134dd4-31c5-46e8-8e86-3608d31af95b"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""7f0e81c3-7122-4b71-becd-a43a66f2e788"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""dc3b191f-3db2-45aa-bf6e-1e8a5e15f29c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Inventory1"",
                    ""type"": ""Button"",
                    ""id"": ""c057ecd4-4e7d-4b83-b438-d3ced7fe06a0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Inventory2"",
                    ""type"": ""Button"",
                    ""id"": ""54ac475f-3c39-4536-a365-1eea02818e72"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Inventory3"",
                    ""type"": ""Button"",
                    ""id"": ""414cf823-189b-4da8-b37f-7d084a06569a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Inventory4"",
                    ""type"": ""Button"",
                    ""id"": ""7a190624-e334-46cd-8718-c17e521ac7a4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Inventory5"",
                    ""type"": ""Button"",
                    ""id"": ""30e7d50d-1791-437e-a1bb-20750f8df1a5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Inventory6"",
                    ""type"": ""Button"",
                    ""id"": ""1322c8ed-e67f-48db-8e3b-d19448e305ec"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Inventory7"",
                    ""type"": ""Button"",
                    ""id"": ""b016c428-0d07-442c-b71a-06400e86dc75"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Inventory8"",
                    ""type"": ""Button"",
                    ""id"": ""b0fa5d87-6421-48c8-b5e9-87173d5475d6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Inventory9"",
                    ""type"": ""Button"",
                    ""id"": ""fa42ba0f-943a-44bd-a1ef-2afcdd3da5e5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Inventory0"",
                    ""type"": ""Button"",
                    ""id"": ""aeade330-cdab-46a8-8bef-c64ff38a7afa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""0279c5a5-cea8-4d53-b949-81b04e8fd04e"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""4f3edc78-13a8-4485-9513-53bd4e1dcaf4"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""00544cb2-08c6-4aa5-8790-b39d00028a20"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""b8ac2d2a-2637-479b-b58d-43afea421b0e"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""7672ae55-098f-4ccc-9546-123d1037d1ab"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""36f78077-0bba-45b4-b5d8-e4be505558e3"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""18627318-e1b1-4194-8c08-5ae4d49b705b"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""e758b6c9-9dcb-4136-96cd-ff61129d7c9e"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""06758bc9-3d7c-4c54-aeed-82f87cbf3b15"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""489bf28e-285e-495f-8e98-61ee06291226"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""fb67ceb2-83f2-428c-8c9f-e023f6a22217"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a7355f61-44c1-44f9-96af-1d9187032325"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventory1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c04216ff-5bca-4fe9-ae58-66cd834144e0"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventory2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""92caa4f2-755f-4085-bffc-b9bb016abf07"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventory3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""764e7a21-7461-4abf-935e-2f2698433b9f"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventory4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2b7d1a50-3698-416a-b411-ea29a2d91613"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventory5"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4c6ca5b5-187f-431d-baea-9df8701ea372"",
                    ""path"": ""<Keyboard>/6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventory6"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e24b395e-3295-4c95-9279-cde1e3802a26"",
                    ""path"": ""<Keyboard>/7"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventory7"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0a56026f-a7ff-40e5-8fcf-69be7ff29778"",
                    ""path"": ""<Keyboard>/8"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventory8"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3f307b4e-93b8-48d3-91b9-aa2440e3827b"",
                    ""path"": ""<Keyboard>/9"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventory9"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a4c1df46-1394-4f77-b3d1-d798d5e4bb45"",
                    ""path"": ""<Keyboard>/0"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventory0"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard and mouse"",
            ""bindingGroup"": ""Keyboard and mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
        m_Player_Inventory1 = m_Player.FindAction("Inventory1", throwIfNotFound: true);
        m_Player_Inventory2 = m_Player.FindAction("Inventory2", throwIfNotFound: true);
        m_Player_Inventory3 = m_Player.FindAction("Inventory3", throwIfNotFound: true);
        m_Player_Inventory4 = m_Player.FindAction("Inventory4", throwIfNotFound: true);
        m_Player_Inventory5 = m_Player.FindAction("Inventory5", throwIfNotFound: true);
        m_Player_Inventory6 = m_Player.FindAction("Inventory6", throwIfNotFound: true);
        m_Player_Inventory7 = m_Player.FindAction("Inventory7", throwIfNotFound: true);
        m_Player_Inventory8 = m_Player.FindAction("Inventory8", throwIfNotFound: true);
        m_Player_Inventory9 = m_Player.FindAction("Inventory9", throwIfNotFound: true);
        m_Player_Inventory0 = m_Player.FindAction("Inventory0", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_Interact;
    private readonly InputAction m_Player_Inventory1;
    private readonly InputAction m_Player_Inventory2;
    private readonly InputAction m_Player_Inventory3;
    private readonly InputAction m_Player_Inventory4;
    private readonly InputAction m_Player_Inventory5;
    private readonly InputAction m_Player_Inventory6;
    private readonly InputAction m_Player_Inventory7;
    private readonly InputAction m_Player_Inventory8;
    private readonly InputAction m_Player_Inventory9;
    private readonly InputAction m_Player_Inventory0;
    public struct PlayerActions
    {
        private @InputMaster m_Wrapper;
        public PlayerActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputAction @Inventory1 => m_Wrapper.m_Player_Inventory1;
        public InputAction @Inventory2 => m_Wrapper.m_Player_Inventory2;
        public InputAction @Inventory3 => m_Wrapper.m_Player_Inventory3;
        public InputAction @Inventory4 => m_Wrapper.m_Player_Inventory4;
        public InputAction @Inventory5 => m_Wrapper.m_Player_Inventory5;
        public InputAction @Inventory6 => m_Wrapper.m_Player_Inventory6;
        public InputAction @Inventory7 => m_Wrapper.m_Player_Inventory7;
        public InputAction @Inventory8 => m_Wrapper.m_Player_Inventory8;
        public InputAction @Inventory9 => m_Wrapper.m_Player_Inventory9;
        public InputAction @Inventory0 => m_Wrapper.m_Player_Inventory0;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Interact.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Inventory1.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory1;
                @Inventory1.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory1;
                @Inventory1.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory1;
                @Inventory2.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory2;
                @Inventory2.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory2;
                @Inventory2.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory2;
                @Inventory3.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory3;
                @Inventory3.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory3;
                @Inventory3.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory3;
                @Inventory4.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory4;
                @Inventory4.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory4;
                @Inventory4.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory4;
                @Inventory5.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory5;
                @Inventory5.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory5;
                @Inventory5.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory5;
                @Inventory6.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory6;
                @Inventory6.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory6;
                @Inventory6.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory6;
                @Inventory7.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory7;
                @Inventory7.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory7;
                @Inventory7.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory7;
                @Inventory8.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory8;
                @Inventory8.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory8;
                @Inventory8.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory8;
                @Inventory9.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory9;
                @Inventory9.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory9;
                @Inventory9.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory9;
                @Inventory0.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory0;
                @Inventory0.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory0;
                @Inventory0.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory0;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Inventory1.started += instance.OnInventory1;
                @Inventory1.performed += instance.OnInventory1;
                @Inventory1.canceled += instance.OnInventory1;
                @Inventory2.started += instance.OnInventory2;
                @Inventory2.performed += instance.OnInventory2;
                @Inventory2.canceled += instance.OnInventory2;
                @Inventory3.started += instance.OnInventory3;
                @Inventory3.performed += instance.OnInventory3;
                @Inventory3.canceled += instance.OnInventory3;
                @Inventory4.started += instance.OnInventory4;
                @Inventory4.performed += instance.OnInventory4;
                @Inventory4.canceled += instance.OnInventory4;
                @Inventory5.started += instance.OnInventory5;
                @Inventory5.performed += instance.OnInventory5;
                @Inventory5.canceled += instance.OnInventory5;
                @Inventory6.started += instance.OnInventory6;
                @Inventory6.performed += instance.OnInventory6;
                @Inventory6.canceled += instance.OnInventory6;
                @Inventory7.started += instance.OnInventory7;
                @Inventory7.performed += instance.OnInventory7;
                @Inventory7.canceled += instance.OnInventory7;
                @Inventory8.started += instance.OnInventory8;
                @Inventory8.performed += instance.OnInventory8;
                @Inventory8.canceled += instance.OnInventory8;
                @Inventory9.started += instance.OnInventory9;
                @Inventory9.performed += instance.OnInventory9;
                @Inventory9.canceled += instance.OnInventory9;
                @Inventory0.started += instance.OnInventory0;
                @Inventory0.performed += instance.OnInventory0;
                @Inventory0.canceled += instance.OnInventory0;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_KeyboardandmouseSchemeIndex = -1;
    public InputControlScheme KeyboardandmouseScheme
    {
        get
        {
            if (m_KeyboardandmouseSchemeIndex == -1) m_KeyboardandmouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard and mouse");
            return asset.controlSchemes[m_KeyboardandmouseSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnInventory1(InputAction.CallbackContext context);
        void OnInventory2(InputAction.CallbackContext context);
        void OnInventory3(InputAction.CallbackContext context);
        void OnInventory4(InputAction.CallbackContext context);
        void OnInventory5(InputAction.CallbackContext context);
        void OnInventory6(InputAction.CallbackContext context);
        void OnInventory7(InputAction.CallbackContext context);
        void OnInventory8(InputAction.CallbackContext context);
        void OnInventory9(InputAction.CallbackContext context);
        void OnInventory0(InputAction.CallbackContext context);
    }
}
