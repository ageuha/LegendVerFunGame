using System;
using KJW.Code.Data;
using UnityEngine;

namespace KJW.Code.Player
{
    public class Player : MonoBehaviour
    {
        public PlayerInput InputCompo {get; private set;}
        public AgentMovement MoveCompo {get; private set;}
        public RollingData RollingData {get; private set;}
        
        private void Awake()
        {
            InputCompo = GetComponent<PlayerInput>();
            MoveCompo = GetComponent<AgentMovement>();
        }
    }
}