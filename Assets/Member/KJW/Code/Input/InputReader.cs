using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static InputSystem_Actions;

namespace Member.KJW.Code.Input
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "SO/InputReader")]
    public class InputReader : ScriptableObject, IPlayerActions
    {
        private InputSystem_Actions _actions;
        public event Action<Vector2> OnMoved;
        public event Action OnAttacked;
        public event Action OnInteracted;
        public event Action OnRolled;

        public Vector2 MousePos { get; private set; }
        public Vector2 Dir { get; private set; }

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
            Dir = context.ReadValue<Vector2>();
            OnMoved?.Invoke(Dir);
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnAttacked?.Invoke();
        }

        public void OnRoll(InputAction.CallbackContext context)
        {
            OnRolled?.Invoke();
        }

        public void OnAim(InputAction.CallbackContext context)
        {
            MousePos = context.ReadValue<Vector2>();
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            OnInteracted?.Invoke();
        }
    }
}
