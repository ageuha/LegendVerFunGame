using Code.Events;
using Member.YDW.Script.NewBuildingSystem;    
using UnityEngine;

namespace Member.YDW.Script
{
    [CreateAssetMenu(fileName = "PathBakeEventSO", menuName = "PathFinder/PathBakeEvent", order = 0)]
    public class PathBakeEventSO : EventChannel<RunTimeBakeEvent>
    {
        
    }
}