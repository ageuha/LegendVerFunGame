using System;
using Code.GridSystem.Map;
using UnityEngine;

namespace Member.YDW.Script
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private int gridSize = 10;
        public GridMap gridMap;

        private void Awake()
        {
            gridMap = new GridMap(gridSize);
        }
    }
}