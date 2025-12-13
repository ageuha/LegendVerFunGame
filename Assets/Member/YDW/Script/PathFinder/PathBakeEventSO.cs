using Code.Events;
using UnityEngine;

namespace Member.YDW.Script.PathFinder
{
    [CreateAssetMenu(fileName = "PathBakeEventSO", menuName = "PathFinder/PathBakeEvent", order = 0)]
    public class PathBakeEventSO : EventChannel<RunTimeBakeEvent>
    {
        
    }
}