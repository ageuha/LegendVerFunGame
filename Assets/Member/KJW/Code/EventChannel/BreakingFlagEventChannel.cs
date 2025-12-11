using Code.Events;
using UnityEngine;

namespace Member.KJW.Code.EventChannel
{
    [CreateAssetMenu(fileName = "BreakingFlagEventChannel", menuName = "EventChannel/BreakingFlagEventChannel", order = 0)]
    public class BreakingFlagEventChannel : EventChannel<bool>
    {
        
    }
}