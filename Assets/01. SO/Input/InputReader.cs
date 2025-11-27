using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static InputSystem_Actions;

namespace _01._SO.Input
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "SO/InputReader")]
    public class InputReader : ScriptableObject, IPlayerActions
    {
        private InputSystem_Actions _actions;
        public event Action<Vector2> OnMoved;
        
        public bool IsDashing { get; private set; }

        private void OnEnable()
        {
            _actions ??= new InputSystem_Actions();
            
            _actions.Player.SetCallbacks(this);
            _actions.Enable();
        }
        
        private void OnDisable()
        {
            _actions.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            OnMoved?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.performed)
                IsDashing = true;
            if (context.canceled)
                IsDashing = false;
        }
    }
}
