using System.Collections.Generic;
using Member.YDW.Script.PathFinder;

namespace Member.YDW.Script.BuildingSystem
{
    public interface IBuilding
    {
        public BuildingDataSO BuildingData { get; }

        public void Initialize(BuildingDataSO buildingData);

        public void DestroyedBuilding();
    }
}