using System;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KJW.Code.Player
{
    public class PlayerInput : MonoBehaviour
    {
        [field: SerializeField] public InputReader InputReader {get; private set;}
        
        public Vector2 MoveDir { get; private set; }
        public Vector2 MousePos { get; private set; }
        public event Action OnAttacked;
        public event Action OnAttackReleased;

        private void Awake()
        {
            InputReader.OnMoved += (dir) => MoveDir = dir;
            InputReader.OnAttacked += () => OnAttacked?.Invoke();
            InputReader.OnAttackReleased += () => OnAttackReleased?.Invoke();
            InputReader.OnAimed += (pos) => MousePos = pos;
        }
    }
}