using System;
using Member.YDW.Script.PathFinder;
using UnityEngine;

namespace Member.YDW.Script
{
    public class SOProvider : MonoBehaviour
    {
        [field:SerializeField] public BakedDataSO _bakedDataSO { get; private set; }
        
        public static SOProvider Instance;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else 
                Destroy(this);
        }
    }
}