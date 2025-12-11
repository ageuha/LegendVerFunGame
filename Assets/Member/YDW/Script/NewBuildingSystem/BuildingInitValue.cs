using System;
using Member.YTH.Code.Item;

namespace Member.YDW.Script.NewBuildingSystem
{
    [Serializable]
    public struct BuildingInitValue //내가 진짜 미안하다. 구조를 정말 개떡같이 짜버려서 이런식으로밖에 안된다.
    {
        public int valueInt;
        public int valueInt2;
        public string valueString;
        public float valueFloat;
        public float valueFloat2;
        public float valueFloat3;
        public ItemDataSO itemData1;
        public int dropCount1;
        public ItemDataSO itemData2;
        public int dropCount2;
        public ItemDataSO itemData3;
        public int dropCount3;


    }
}