using UnityEngine;

namespace Member.YDW.Script.RequestSystem.Values
{
    public struct BakeDataSOValueM : IRequestValue
    {
        public Vector3 worldPosition;
        public Vector3Int cellPosition;

        public BakeDataSOValueM(Vector3 worldPosition)
        {
            this.worldPosition = worldPosition;
            cellPosition = default;
        }

        public BakeDataSOValueM(Vector3Int cellPosition)
        {
            this.cellPosition =  cellPosition;
            worldPosition = default;
        }
    }
}