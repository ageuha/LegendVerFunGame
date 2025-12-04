using System.Collections.Generic;
using Code.Events;
using Member.YDW.Script.PathFinder;
using UnityEngine;

namespace Member.YDW.Script
{
    [CreateAssetMenu(fileName = "PathBakeEventSO", menuName = "PathFinder/PathBakeEvent", order = 0)]
    public class PathBakeEventSO : EventChannel<List<NodeData>>
    {
        
    }
}