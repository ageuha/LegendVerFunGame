using System;
using UnityEngine;

namespace Member.YDW.Script.PathFinder
{
    public class AstarNode : IComparable<AstarNode>
    {
        public Vector3 worldPosition;
        public Vector3Int cellPosition;
        public NodeData nodeData;

        public AstarNode parent;

        public float G;
        public float F; // F = G + H
        public int CompareTo(AstarNode other)
        {
            if(Mathf.Approximately(other.F, F)) return 0;

            return other.F < F ? -1 : 1;
        }
        public override int GetHashCode() => cellPosition.GetHashCode();
        public override bool Equals(object obj)
        {
            if (obj is AstarNode astarNode)
            {
                return astarNode.cellPosition == cellPosition;
            }
            return false;
        }
   
    }
}