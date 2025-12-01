using System;
using KJW.Code.Input;
using KJW.Code.Data;
using UnityEngine;

namespace KJW.Code.Player
{
    public class Player : MonoBehaviour
    {
        [field: SerializeField] public InputReader InputReader { get; private set; }
        [field: SerializeField] public RollingData RollingData { get; private set; }
        public AgentMovement MoveCompo {get; private set;}
        public bool IsRolling { get; set; }

        private Vector2 _standDir = Vector2.right;
        public Vector2 StandDir
        {
            get
            {
                if (InputReader.Dir != Vector2.zero) _standDir = InputReader.Dir;
                return _standDir;
            }
        }
        
        private void Awake()
        {
            MoveCompo = GetComponent<AgentMovement>();

            InputReader.OnRolled += Roll;
        }

        private void Roll()
        {
            IsRolling = true;
        }
    }
}