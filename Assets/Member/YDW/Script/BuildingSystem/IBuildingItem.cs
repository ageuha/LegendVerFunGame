using UnityEngine;

namespace Member.YDW.Script.BuildingSystem
{
    public interface IBuildingItem
    {
        public BuildingSOEvents EventSO { get; }
        public Sprite Icon { get; }
        
        public void OnBuildingGhost();
        
        public void OffBuildingGhost();
    }
}