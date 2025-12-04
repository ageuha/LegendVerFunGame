using System.Collections.Generic;
using Member.YDW.Script.BuildingSystem;
using Member.YDW.Script.PathFinder;

namespace Member.YDW.Script
{
    public enum NodeDataManagerEventType
    {
        Add,
        Remove,
    }
    public struct NodeDataManagerEvent
    {
        public NodeDataManagerEventType eventType;
        public ObjectType objectType;
        public IBuilding building;
        public IInteractable interactable;
        public List<NodeData> nodeData;

        public NodeDataManagerEvent(NodeDataManagerEventType eventType, ObjectType objectType,IBuilding building,List<NodeData> points)
        {
            this.eventType = eventType;
            this.objectType = objectType;
            this.building = building;
            interactable = null;
            nodeData = points;
        }
        public NodeDataManagerEvent(NodeDataManagerEventType eventType, ObjectType objectType,IInteractable interactable,List<NodeData> points)
        {
            this.eventType = eventType;
            this.objectType = objectType;
            building = null;
            this.interactable = interactable;
            nodeData = points;
        }

        
        
    }
}