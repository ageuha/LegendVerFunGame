using Code.Events;
using Member.YDW.Script.EventStruct;
using UnityEngine;

namespace Member.YDW.Script
{
    [CreateAssetMenu(fileName = "NodeDataManager", menuName = "BuildingSystem/NodeDataManager", order = 0)]
    public class NodeDataManagerEventSO : EventChannel<NodeDataManagerEvent>
    {
        
    }
}