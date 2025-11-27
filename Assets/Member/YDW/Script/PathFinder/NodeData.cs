using System;
using System.Collections.Generic;
using UnityEngine;

namespace _02._Member.YDW.Script.PathFinder
{
    [Serializable]
    public struct NodeData 
    {
        public Vector3 worldPosition;
        public Vector3Int cellPosition;
        public List<LinkData> neighbors;

        public NodeData(Vector3 worldPosition, Vector3Int cellPosition)
        {
            this.worldPosition = worldPosition;
            this.cellPosition = cellPosition;
            neighbors = new List<LinkData>();
        }

        public void AddNeighbors(NodeData neighborNode)
        {
            neighbors.Add(new LinkData
            {
                startPosition = worldPosition,
                startCellPosition = cellPosition,
                endPosition = neighborNode.worldPosition,
                endCellPositon = neighborNode.cellPosition,
                cost = Vector3Int.Distance(cellPosition, neighborNode.cellPosition)
            });
        }

        public override int GetHashCode() => cellPosition.GetHashCode();

        public override bool Equals(object obj)
        {
            if (obj is NodeData nodeData)
            {
                return nodeData.cellPosition == cellPosition;
            }
            return false;
        }
    
        public static bool operator ==(NodeData lhs, NodeData rhs)
        {
            return  lhs.Equals(rhs);
        }

        public static bool operator !=(NodeData lhs, NodeData rhs)
        {
            return !lhs.Equals(rhs);
        }



    }
}