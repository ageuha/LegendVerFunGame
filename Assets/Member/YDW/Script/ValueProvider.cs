using System;
using System.Collections.Generic;
using Member.YDW.Script.PathFinder;
using UnityEngine;

namespace Member.YDW.Script
{
    public class ValueProvider : MonoBehaviour
    {
        [field:SerializeField] public BakedDataSO _bakedDataSO { get; private set; }
        
        public static ValueProvider Instance;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else 
                Destroy(this);
        }
    }
}