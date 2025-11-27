using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _02._Member.YDW.Script.PathFinder
{
    [CreateAssetMenu(fileName = "BakedDataSO", menuName = "Dowon/Path/BakedData")]
    public class BakedDataSO : ScriptableObject
    {
        public List<NodeData> points = new();
        public List<Vector3Int> SetCanBuildPoints = new();
        private Dictionary<Vector3Int, NodeData> _pointDict;
        public bool SetCanBuildValue;


        private void OnEnable()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (_pointDict == null || _pointDict.Count != points.Count)
                _pointDict = points.ToDictionary(node => node.cellPosition);
        }
            
        public void ClearPoints() => points.Clear();

        public void AddPoint(Vector3 worldPosition, Vector3Int cellPosition,bool CanBuild) => points.Add(new NodeData(worldPosition, cellPosition,CanBuild));
   
        public bool HasNode(Vector3Int cellPosition) => _pointDict != null && _pointDict.ContainsKey(cellPosition); // �� ���� �ڸ��� ��ġ�ϱ� ������ �ڵ����� bool���� ��ȯ.

        public bool TryGetNode(Vector3Int cellPosition, out NodeData nodeData)
        {
            if (HasNode(cellPosition))
            {
                nodeData = _pointDict[cellPosition];
                return true;
            }

            nodeData = default;
            return false;
        }

        public bool CheckCanBuild(Vector3Int cellPosition)
        {
            if (_pointDict.TryGetValue(cellPosition, out NodeData node))
            {
                return node.canBuild;
            }
            return false;
        }

        public bool SetCanBuild(List<Vector3Int> cellPoints, bool canBuild)
        {
            SetCanBuildValue = canBuild;
            foreach (Vector3Int point in cellPoints)
            {
                SetCanBuildPoints.Add(point);
            }
            return false;
        }
    }
}