using System;
using _01._SO;
using UnityEngine;

namespace _02._Member.KJW.Code.Player
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private InputReader inputReader;
        
        public event Action<Vector2> OnMoved;

        private void Awake()
        {
            inputReader.OnMoved += (v) => OnMoved?.Invoke(v);
        }
    }
}
