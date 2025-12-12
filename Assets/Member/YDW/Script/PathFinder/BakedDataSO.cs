using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Member.YDW.Script.PathFinder
{
    [CreateAssetMenu(fileName = "BakedDataSO", menuName = "PathFinder/Path/BakedData")]
    public class BakedDataSO : ScriptableObject
    {
        public List<NodeData> points = new();
        private Dictionary<Vector3Int, NodeData> _pointDict;
        private Dictionary<Vector3,NodeData> _worldPointDict;


        private void OnEnable()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (_pointDict == null || _pointDict.Count != points.Count)
                _pointDict = points.ToDictionary(node => node.cellPosition);
            if (_worldPointDict == null || _worldPointDict.Count != points.Count)
                _worldPointDict = points.ToDictionary(node => node.worldPosition);


        }
            
        public void ClearPoints() => points.Clear();

        public void AddPoint(Vector3 worldPosition, Vector3Int cellPosition) => points.Add(new NodeData(worldPosition, cellPosition));
   
        public bool HasNode(Vector3Int cellPosition) => _pointDict != null && _pointDict.ContainsKey(cellPosition);
        public bool HasNode(Vector3 worldPosition) => _worldPointDict != null && _worldPointDict.ContainsKey(worldPosition);

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
    }
}