using System;
using System.Collections.Generic;
using Code.Core.Utility;
using Member.YDW.Script.PathFinder;
using UnityEngine;

namespace Member.YDW.Script
{
    public class ValueProvider : MonoBehaviour
    {
        [field:SerializeField] public BakedDataSO BakedDataSO { get; private set; }
        
        public static ValueProvider Instance;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else 
                Destroy(this);
            
            if(BakedDataSO == null)
                Logging.Log("ValueProvider에 필요한 값이 모두 들어있지 않습니다. 확인하세요.");
        }
    }
}