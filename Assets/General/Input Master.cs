// GENERATED AUTOMATICALLY FROM 'Assets/General/Input Master.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Input Master"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""4c9dc808-b124-4852-9269-cad922ac84ea"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""6fc6c8a7-9548-4060-bfa3-29fb876b39f7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""0c63b83b-25b9-4532-91f8-62b89f4a547b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""eb09674e-4df6-445e-8519-946699145433"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Extended_Attack"",
                    ""type"": ""Button"",
                    ""id"": ""b048a396-dd68-481e-acc9-6eff0dfb4cfa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SlowdownBomb"",
                    ""type"": ""Button"",
                    ""id"": ""fd571df9-a0f4-49ae-b443-805f76f4b45d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SpinAttack"",
                    ""type"": ""Button"",
                    ""id"": ""841f0241-8e55-4502-aa52-4dbe3d961697"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CloneAttack"",
                    ""type"": ""Button"",
                    ""id"": ""110a3d37-1f2b-4fa9-88e9-633a3b4b6cc0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AccelerateTime"",
                    ""type"": ""Button"",
                    ""id"": ""3d3eee31-586a-4663-9a0c-f51874ca4d36"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ba30561c-58f5-42bb-96d2-c61991e84590"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d0cfbb16-1c66-4774-b400-84949ddb036a"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""a5a6623a-c503-4f88-b2a2-2a6be3d0629a"",
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
                    ""id"": ""5d383f72-a146-4bd2-9ddc-0fe69014c820"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ae7de5f7-c1d3-4fa8-9ece-1d8305b34b2a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""92869853-17a8-40cf-a79a-beacd589b455"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""451a4fb0-d824-46e0-b049-a777722a481f"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""ed3364f2-fa9a-4e82-a675-7e1950af4d0f"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Extended_Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2a406650-7a5c-4d42-b64d-02958b237f7b"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SpinAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3005c11a-17c7-4010-a8a6-43e45412c3cc"",
                    ""path"": ""<Keyboard>/o"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""CloneAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""55db3498-0e4f-4aa7-a7bd-baab16563086"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AccelerateTime"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""41870569-0e59-4fba-a17b-a8b4cf9f4e1d"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SlowdownBomb"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9fa80d9e-f145-4df9-a959-c507f8a67a8e"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
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
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Attack = m_Player.FindAction("Attack", throwIfNotFound: true);
        m_Player_Extended_Attack = m_Player.FindAction("Extended_Attack", throwIfNotFound: true);
        m_Player_SlowdownBomb = m_Player.FindAction("SlowdownBomb", throwIfNotFound: true);
        m_Player_SpinAttack = m_Player.FindAction("SpinAttack", throwIfNotFound: true);
        m_Player_CloneAttack = m_Player.FindAction("CloneAttack", throwIfNotFound: true);
        m_Player_AccelerateTime = m_Player.FindAction("AccelerateTime", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Attack;
    private readonly InputAction m_Player_Extended_Attack;
    private readonly InputAction m_Player_SlowdownBomb;
    private readonly InputAction m_Player_SpinAttack;
    private readonly InputAction m_Player_CloneAttack;
    private readonly InputAction m_Player_AccelerateTime;
    public struct PlayerActions
    {
        private @InputMaster m_Wrapper;
        public PlayerActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Attack => m_Wrapper.m_Player_Attack;
        public InputAction @Extended_Attack => m_Wrapper.m_Player_Extended_Attack;
        public InputAction @SlowdownBomb => m_Wrapper.m_Player_SlowdownBomb;
        public InputAction @SpinAttack => m_Wrapper.m_Player_SpinAttack;
        public InputAction @CloneAttack => m_Wrapper.m_Player_CloneAttack;
        public InputAction @AccelerateTime => m_Wrapper.m_Player_AccelerateTime;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Attack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Extended_Attack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnExtended_Attack;
                @Extended_Attack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnExtended_Attack;
                @Extended_Attack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnExtended_Attack;
                @SlowdownBomb.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSlowdownBomb;
                @SlowdownBomb.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSlowdownBomb;
                @SlowdownBomb.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSlowdownBomb;
                @SpinAttack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpinAttack;
                @SpinAttack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpinAttack;
                @SpinAttack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpinAttack;
                @CloneAttack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCloneAttack;
                @CloneAttack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCloneAttack;
                @CloneAttack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCloneAttack;
                @AccelerateTime.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAccelerateTime;
                @AccelerateTime.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAccelerateTime;
                @AccelerateTime.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAccelerateTime;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Extended_Attack.started += instance.OnExtended_Attack;
                @Extended_Attack.performed += instance.OnExtended_Attack;
                @Extended_Attack.canceled += instance.OnExtended_Attack;
                @SlowdownBomb.started += instance.OnSlowdownBomb;
                @SlowdownBomb.performed += instance.OnSlowdownBomb;
                @SlowdownBomb.canceled += instance.OnSlowdownBomb;
                @SpinAttack.started += instance.OnSpinAttack;
                @SpinAttack.performed += instance.OnSpinAttack;
                @SpinAttack.canceled += instance.OnSpinAttack;
                @CloneAttack.started += instance.OnCloneAttack;
                @CloneAttack.performed += instance.OnCloneAttack;
                @CloneAttack.canceled += instance.OnCloneAttack;
                @AccelerateTime.started += instance.OnAccelerateTime;
                @AccelerateTime.performed += instance.OnAccelerateTime;
                @AccelerateTime.canceled += instance.OnAccelerateTime;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnExtended_Attack(InputAction.CallbackContext context);
        void OnSlowdownBomb(InputAction.CallbackContext context);
        void OnSpinAttack(InputAction.CallbackContext context);
        void OnCloneAttack(InputAction.CallbackContext context);
        void OnAccelerateTime(InputAction.CallbackContext context);
    }
}
