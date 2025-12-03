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
        [field: SerializeField] public Interactor Interactor { get; private set; }
        public AgentMovement MoveCompo {get; private set;}
        public bool IsRolling { get; set; }

        public Vector2 StandDir { get; private set; } = Vector2.right;
        
        private void Awake()
        {
            MoveCompo = GetComponent<AgentMovement>();
            Interactor = GetComponent<Interactor>();

            InputReader.OnRolled += Roll;
            InputReader.OnMoved += UpdateStandDir;
            InputReader.OnInteracted += Interactor.Interact;
        }

        private void UpdateStandDir(Vector2 dir)
        {
            if (dir != Vector2.zero) StandDir = dir;
        }

        private void OnDisable()
        {
            InputReader.OnRolled -= Roll;
            InputReader.OnInteracted -= Interactor.Interact;
        }

        private void Roll()
        {
            IsRolling = true;
        }
    }
}