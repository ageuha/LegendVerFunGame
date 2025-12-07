using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Member.YDW.Script.PathFinder
{
    public class PathAgent : MonoBehaviour
    {
        [SerializeField] private BakedDataSO bakedData;

        private PriorityQueue<AstarNode> _openList = new PriorityQueue<AstarNode>();
        private List<AstarNode> _closeList = new();
        private List<AstarNode> _path = new();

        public int GetPath(Vector3Int startPosition, Vector3Int destination, Vector3[] pointArr)
        {
            if (CalculatePath(startPosition, destination))
            {
                int cornerIdx = 0;

                pointArr[cornerIdx] = _path[0].worldPosition;
                cornerIdx++;

                for (int i = 1; i < _path.Count - 1; i++)
                {
                    if (cornerIdx >= pointArr.Length) break;

                    Vector3Int beforeDirection = _path[i].cellPosition - _path[i - 1].cellPosition;
                    Vector3Int nextDirection = _path[i + 1].cellPosition - _path[i].cellPosition;

                    if (beforeDirection != nextDirection)
                    {
                        pointArr[cornerIdx] = _path[i].worldPosition;
                        cornerIdx++;
                    }
                }
                pointArr[cornerIdx] = _path[^1].worldPosition;
                cornerIdx++;

                return cornerIdx;
            }
            return 0;
        }

        private bool CalculatePath(Vector3Int startPosition, Vector3Int destination)
        {
            _openList.Clear();
            _closeList.Clear();
            _path.Clear();

            bool result = false;
            if(bakedData.TryGetNode(startPosition, out NodeData startNode) == false)
                return false;
            if(bakedData.TryGetNode(destination, out NodeData endNode) == false)
                return false;

            _openList.Push(new AstarNode
            {
                nodeData = startNode,
                cellPosition = startNode.cellPosition,
                worldPosition = startNode.worldPosition,
                parent = null,
                G = 0, F = CalcH(startNode.cellPosition,endNode.cellPosition)

            });

            while (_openList.Count > 0)
            {
                AstarNode currentNode = _openList.Pop();

                foreach (LinkData link in currentNode.nodeData.neighbors)
                {
                    bool isVisited = _closeList.Any(node => node.cellPosition == link.endCellPositon);
                    if (isVisited) continue;

                    if (bakedData.TryGetNode(link.endCellPositon, out NodeData nextNode) == false)
                        continue;
               

                    float newG = link.cost + currentNode.G;

                    AstarNode nextAstarNode = new AstarNode()
                    {
                        nodeData = nextNode,
                        cellPosition = nextNode.cellPosition,
                        worldPosition = nextNode.worldPosition,
                        parent = currentNode,
                        G = newG, F = newG + CalcH(nextNode.cellPosition,endNode.cellPosition)
                    };

                    AstarNode existInOpenList = _openList.Contains(nextAstarNode);
                    if (existInOpenList != null)
                    {
                        //f: g+ h g: 쓴 비용 h:앞으로
                        if (nextAstarNode.G < existInOpenList.G)
                        {
                            existInOpenList.G = nextAstarNode.G;
                            existInOpenList.F = nextAstarNode.F;
                            existInOpenList.parent = nextAstarNode.parent;
                        }
                    }
                    else
                    {
                        _openList.Push(nextAstarNode);
                    }
                }

                _closeList.Add(currentNode);

                if (currentNode.nodeData == endNode)
                {
                    result = true;
                    break;
                }
            }

            if (result)
            {
                AstarNode last = _closeList[^1];
                while (last.parent != null)
                {
                    _path.Add(last);
                    last = last.parent;
                }
                _path.Add(last);
                _path.Reverse();
            }

            return result;
        }





        private float CalcH(Vector3Int startCellPosition, Vector3Int endCellPosition)
            => Vector3Int.Distance(startCellPosition, endCellPosition);
  
    }
}
