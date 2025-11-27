using System;
using UnityEngine;

namespace _02._Member.YDW.Script.PathFinder
{
    [Serializable]
    public struct LinkData
    {
        public Vector3 startPosition;
        public Vector3Int startCellPosition;
        public Vector3 endPosition;
        public Vector3Int endCellPositon;

        public float cost;
    }
}