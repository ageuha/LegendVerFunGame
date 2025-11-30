using UnityEngine;

namespace Member.YDW.Script.BuildingSystem.EventStruct
{
    public struct BuildingGhostEvent
    {
        public bool OnOff;
        public Sprite Image;

        public BuildingGhostEvent(Sprite image, bool onOff)
        {
            OnOff = onOff;
            Image = image;
        }
    }
}