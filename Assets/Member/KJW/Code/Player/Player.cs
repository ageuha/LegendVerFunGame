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
            InputReader.OnInteracted += Interactor.Interact;
        }

        private void Update()
        {
            if (InputReader.Dir != Vector2.zero) StandDir = InputReader.Dir;
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