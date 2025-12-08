using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static InputSystem_Actions;

namespace Member.KJW.Code.Input
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "SO/InputReader")]
    public class InputReader : ScriptableObject, IPlayerActions
    {
        public InputSystem_Actions Actions { get; private set; }
        public event Action<Vector2> OnMoved;
        public event Action OnAttacked;
        public event Action OnAttackReleased;
        public event Action OnThrew;
        public event Action OnThrewReleased;
        public event Action OnPlaced;
        public event Action OnPlaceReleased;
        public event Action OnInteracted;
        public event Action OnRolled;
        public event Action<float> OnScrolled;
        public event Action<int> OnNumKeyPressed;
        public event Action OnInventory;

        public Vector2 MousePos { get; private set; }
        public Vector2 Dir { get; private set; }

        private void OnEnable()
        {
            Actions ??= new InputSystem_Actions();
            
            Actions.Player.SetCallbacks(this);
            Actions.Enable();
        }
        
        private void OnDisable()
        {
            Actions.Disable();
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
            if (context.canceled)
                OnAttackReleased?.Invoke();
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

        public void OnThrow(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnThrew?.Invoke();
            if (context.canceled)
                OnThrewReleased?.Invoke();
        }

        public void OnPlace(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnPlaced?.Invoke();
            if (context.canceled)
                OnPlaceReleased?.Invoke();
        }

        public void OnScroll(InputAction.CallbackContext context)
        {
            OnScrolled?.Invoke(context.ReadValue<float>());
        }

        public void OnOneKey(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnNumKeyPressed?.Invoke(1);
        }

        public void OnTwoKey(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnNumKeyPressed?.Invoke(2);
        }

        public void OnThreeKey(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnNumKeyPressed?.Invoke(3);
        }

        public void OnFourKey(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnNumKeyPressed?.Invoke(4);
        }

        public void OnFiveKey(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnNumKeyPressed?.Invoke(5);
        }

        public void OnSixKey(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnNumKeyPressed?.Invoke(6);
        }

        public void OnSevenKey(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnNumKeyPressed?.Invoke(7);
        }

        public void OnEightKey(InputAction.CallbackContext context)
        {   
            if (context.performed)
                OnNumKeyPressed?.Invoke(8);
        }

        public void OnNineKey(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnNumKeyPressed?.Invoke(9);
        }

        public void OnInventoryOnOff(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnInventory?.Invoke();
        }
    }
}
