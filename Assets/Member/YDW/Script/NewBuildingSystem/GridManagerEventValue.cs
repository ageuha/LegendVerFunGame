using Code.GridSystem.Objects;
using Member.YDW.Script.BuildingSystem;

namespace Member.YDW.Script.NewBuildingSystem
{
    public enum GridManagerEventType
    {
        Set,
        Delete
    }
    public struct GridManagerEventValue //빌딩 메니저 쪽에서 이벤트를 받아서 설치하는 방식.
    {
        public GridManagerEventType eventType;
        public GridObject gridObject;

        public GridManagerEventValue(GridManagerEventType eventType,GridObject gridObject)
        {
            this.eventType =  eventType;
            this.gridObject = gridObject;
        }
    }
}