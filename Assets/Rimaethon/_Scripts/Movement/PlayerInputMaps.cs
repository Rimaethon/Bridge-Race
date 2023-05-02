
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInputMaps: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputMaps()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputMaps"",
    ""maps"": [
        {
            ""name"": ""PlayerTouch"",
            ""id"": ""fb08f9ed-d6a6-4dc7-b5a4-2dfca5512c75"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""2aa56102-949e-460c-9905-0c736c9cee37"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""FirstTouch"",
                    ""type"": ""PassThrough"",
                    ""id"": ""3e90011a-1220-41c3-a4c5-1510d89d2b39"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""TouchHolding"",
                    ""type"": ""Button"",
                    ""id"": ""c850efc4-be13-40d5-884d-ff5bb343e5aa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""31d434e3-8b6b-4583-85a7-c13abba61a33"",
                    ""path"": ""<Touchscreen>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d642ee4f-b221-46dd-8980-d69299bbec02"",
                    ""path"": ""<Touchscreen>/primaryTouch/startPosition"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FirstTouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""afc82d68-a5d1-44d6-a33c-20fe5f0ab6e4"",
                    ""path"": ""<Touchscreen>/Press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchHolding"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerTouch
        m_PlayerTouch = asset.FindActionMap("PlayerTouch", throwIfNotFound: true);
        m_PlayerTouch_Move = m_PlayerTouch.FindAction("Move", throwIfNotFound: true);
        m_PlayerTouch_FirstTouch = m_PlayerTouch.FindAction("FirstTouch", throwIfNotFound: true);
        m_PlayerTouch_TouchHolding = m_PlayerTouch.FindAction("TouchHolding", throwIfNotFound: true);
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

    // PlayerTouch
    private readonly InputActionMap m_PlayerTouch;
    private List<IPlayerTouchActions> m_PlayerTouchActionsCallbackInterfaces = new List<IPlayerTouchActions>();
    private readonly InputAction m_PlayerTouch_Move;
    private readonly InputAction m_PlayerTouch_FirstTouch;
    private readonly InputAction m_PlayerTouch_TouchHolding;
    public struct PlayerTouchActions
    {
        private @PlayerInputMaps m_Wrapper;
        public PlayerTouchActions(@PlayerInputMaps wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PlayerTouch_Move;
        public InputAction @FirstTouch => m_Wrapper.m_PlayerTouch_FirstTouch;
        public InputAction @TouchHolding => m_Wrapper.m_PlayerTouch_TouchHolding;
        public InputActionMap Get() { return m_Wrapper.m_PlayerTouch; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerTouchActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerTouchActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerTouchActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerTouchActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @FirstTouch.started += instance.OnFirstTouch;
            @FirstTouch.performed += instance.OnFirstTouch;
            @FirstTouch.canceled += instance.OnFirstTouch;
            @TouchHolding.started += instance.OnTouchHolding;
            @TouchHolding.performed += instance.OnTouchHolding;
            @TouchHolding.canceled += instance.OnTouchHolding;
        }

        private void UnregisterCallbacks(IPlayerTouchActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @FirstTouch.started -= instance.OnFirstTouch;
            @FirstTouch.performed -= instance.OnFirstTouch;
            @FirstTouch.canceled -= instance.OnFirstTouch;
            @TouchHolding.started -= instance.OnTouchHolding;
            @TouchHolding.performed -= instance.OnTouchHolding;
            @TouchHolding.canceled -= instance.OnTouchHolding;
        }

        public void RemoveCallbacks(IPlayerTouchActions instance)
        {
            if (m_Wrapper.m_PlayerTouchActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerTouchActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerTouchActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerTouchActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerTouchActions @PlayerTouch => new PlayerTouchActions(this);
    public interface IPlayerTouchActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnFirstTouch(InputAction.CallbackContext context);
        void OnTouchHolding(InputAction.CallbackContext context);
    }
}
