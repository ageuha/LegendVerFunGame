using System;
using UnityEngine;

namespace KJW.Code.Player
{
    public class Player : MonoBehaviour
    {
        public PlayerInput InputCompo {get; private set;}
        public PlayerMovement MoveCompo {get; private set;}

        public bool CanDash { get; private set; }

        private void Awake()
        {
            InputCompo = GetComponent<PlayerInput>();
            MoveCompo = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            MoveCompo.SetMove(InputCompo.MoveDir);
        }
    }
}