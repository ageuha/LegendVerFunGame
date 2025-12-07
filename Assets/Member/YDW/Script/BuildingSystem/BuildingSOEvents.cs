using System;
using System.Collections.Generic;
using Code.Events;
using UnityEngine;

namespace Member.YDW.Script.BuildingSystem
{
    [CreateAssetMenu(fileName = "BuildingEvents", menuName = "BuildingSystem/Events", order = 0)]
    public class BuildingSOEvents : ScriptableObject
    {
        [field: SerializeField] public BuildingGhostEventSO GhostEventSO { get; private set; }
        
    }
}