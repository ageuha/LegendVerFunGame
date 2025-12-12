using System;
using Member.KJW.Code.Input;
using UnityEngine;

namespace Member.KJW.Code.Test
{
    public class TestGridPointer : MonoBehaviour
    {
        [SerializeField] private InputReader inputReader;
        private Vector2 MouseWorldPos => Camera.main!.ScreenToWorldPoint(inputReader.MousePos);
        private Vector2 _mouseGridPos;
        private Vector2 _previousMouseGridPos;
        
        private void Update()
        {
            _mouseGridPos = Vector2Int.RoundToInt(MouseWorldPos - new Vector2(0.5f, 0.5f));
            
            if (_previousMouseGridPos == _mouseGridPos) return;

            transform.position = _mouseGridPos + new Vector2(0.5f, 0.5f);
            
            _previousMouseGridPos = _mouseGridPos;
        }
    }
}