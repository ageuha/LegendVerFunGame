using System.Collections.Generic;
using Code.Core.Utility;
using Member.YDW.Script.BuildingSystem;
using Member.YDW.Script.EventStruct;
using Member.YDW.Script.PathFinder;
using UnityEngine;

namespace Member.YDW.Script
{
    public enum ObjectType
    {
        Building,
        Resource,
    }
    public class NodeDataManager : MonoBehaviour
    {
        [field: SerializeField] public NodeDataManagerEventSO eventSO { get; private set; }
        private Dictionary<IBuilding,List<NodeData>> _buildingNodeData = new();
        private Dictionary<IInteractable,List<NodeData>> _interactableObjectNodeData = new();

        private void Awake()
        {
            eventSO.OnEvent += HandleNodeDataManagerEvent;
        }

        private void HandleNodeDataManagerEvent(NodeDataManagerEvent obj)
        {
            switch (obj.eventType)
            {
                case NodeDataManagerEventType.Add:
                    AddObject(obj);
                    break;
                case NodeDataManagerEventType.Remove:
                    RemoveObject(obj);
                    break;
            }
        }

        private void RemoveObject(NodeDataManagerEvent obj)
        {
            switch (obj.objectType)
            {
                case ObjectType.Building:
                    _buildingNodeData.Remove(obj.building);
                    break;
                case ObjectType.Resource:
                    _interactableObjectNodeData.Remove(obj.interactable);
                    break;
            }
        }

        private void AddObject(NodeDataManagerEvent obj)
        {
           
            switch (obj.objectType)
            {
                case ObjectType.Building:
                    if (_buildingNodeData.ContainsKey(obj.building))
                    {
                        Logging.Log("이미 해당 건물의 위치값이 NodeManager에 존재합니다.");
                        return;
                    }
                    _buildingNodeData.Add(obj.building, obj.nodeDatas);
                    break;
                case ObjectType.Resource:
                    if (_interactableObjectNodeData.ContainsKey(obj.interactable))
                    {
                        Logging.Log("이미 해당 오브젝트의 위치값이 NodeManager에 존재합니다.");
                        return;
                    }
                    _interactableObjectNodeData.Add(obj.interactable, obj.nodeDatas);
                    break;
            }
        }

        public bool HasObject(NodeData selectData)
        {
            bool flag = false;
            foreach (var nodeData in _buildingNodeData.Values)
            {
                if (nodeData.Contains(selectData))
                {
                    flag = true;
                }
            }
            if(flag)
                return true;
            foreach (var nodeData in _interactableObjectNodeData.Values)
            {
                if (nodeData.Contains(selectData))
                {
                    flag = true;
                }
            }
            return flag;
        }
            
            


        private void OnDestroy()
        {
            eventSO.OnEvent -= HandleNodeDataManagerEvent;
        }
    }
}