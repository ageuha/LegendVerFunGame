using System;
using UnityEngine;

namespace KJW.Code.Player
{
    public class Player : MonoBehaviour
    {
        public PlayerInput InputCompo {get; private set;}
        public AgentMovement MoveCompo {get; private set;}
        
        private void Awake()
        {
            InputCompo = GetComponent<PlayerInput>();
            MoveCompo = GetComponent<AgentMovement>();
        }

        private void Update()
        {
            MoveCompo.SetMove(InputCompo.MoveDir);
        }
        
    }
}