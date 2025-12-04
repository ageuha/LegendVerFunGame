using Code.Events;
using UnityEngine;

namespace Member.YDW.Script
{
    [CreateAssetMenu(fileName = "NodeDataManager", menuName = "BuildingSystem/NodeDataManager", order = 0)]
    public class NodeDataManagerEventSO : EventChannel<NodeDataManagerEvent>
    {
        
    }
}