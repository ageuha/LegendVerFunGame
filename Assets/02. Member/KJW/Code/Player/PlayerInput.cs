using System;
using _01._SO.Input;
using UnityEngine;

namespace _02._Member.KJW.Code.Player
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private InputReader inputReader;
        public bool IsDashing => inputReader.IsDashing;
        
        public Vector2 MoveDir { get; private set; }

        private void Awake()
        {
            inputReader.OnMoved += (dir) => MoveDir = dir;
        }
    }
}
