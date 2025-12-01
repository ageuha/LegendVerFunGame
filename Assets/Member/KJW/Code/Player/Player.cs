using System;
using KJW.Code.Input;
using KJW.Code.Data;
using UnityEngine;

namespace KJW.Code.Player
{
    public class Player : MonoBehaviour
    {
        [field: SerializeField] public InputReader InputReader { get; private set; }
        [SerializeField] private RollingData rollingData;
        public AgentMovement MoveCompo {get; private set;}
        
        
        private void Awake()
        {
            MoveCompo = GetComponent<AgentMovement>();
        }
    }
}