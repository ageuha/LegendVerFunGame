using System;
using System.Collections.Generic;
using Member.YDW.Script.BuildingSystem;
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
        [SerializeField] private NodeDataManagerEventSO eventSO;
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

        private void RemoveObject(NodeDataManagerEvent nodeDataManagerEvent)
        {
            
        }

        private void AddObject(NodeDataManagerEvent nodeDataManagerEvent)
        {
            
        }


        private void OnDestroy()
        {
            eventSO.OnEvent -= HandleNodeDataManagerEvent;
        }
    }
}