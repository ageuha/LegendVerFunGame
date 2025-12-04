using Code.Events;
using UnityEngine;

namespace Member.JJW.EventChannels
{
    [CreateAssetMenu(fileName = "TransformEventChannel", menuName = "EventChannel/TransformEventChannel", order = 0)]
    public class TransformEventChannel : EventChannel<Transform>
    {
        
    }
    
}