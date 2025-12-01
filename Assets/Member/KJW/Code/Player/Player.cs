using System;
using Input;
using KJW.Code.Data;
using UnityEngine;

namespace KJW.Code.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private InputReader inputReader;
        public AgentMovement MoveCompo {get; private set;}
        public RollingData RollingData {get; private set;}
        
        private void Awake()
        {
            MoveCompo = GetComponent<AgentMovement>();
        }
    }
}