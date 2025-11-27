using System;
using _SO.Input;
using UnityEngine;

namespace KJW.Code.Player
{
    public class PlayerInput : MonoBehaviour
    {
        [field: SerializeField] public InputReader InputReader {get; private set;}
        
        public Vector2 MoveDir { get; private set; }

        private void Awake()
        {
            InputReader.OnMoved += (dir) => MoveDir = dir;
        }
    }
}
