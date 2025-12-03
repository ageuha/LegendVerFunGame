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

        public Vector2 StandDir { get; private set; } = Vector2.right;
        
        private void Awake()
        {
            MoveCompo = GetComponent<AgentMovement>();

            InputReader.OnRolled += Roll;
            InputReader.OnMoved += UpdateStandDir;
        }

        private void UpdateStandDir(Vector2 value)
        {
            if (value != Vector2.zero) StandDir = value;
        }

        private void Roll()
        {
            IsRolling = true;
        }
    }
}