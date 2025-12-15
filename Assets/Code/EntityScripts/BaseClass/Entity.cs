using System;
using System.Collections.Generic;
using System.Linq;
using Code.Core.Pool;
using Code.Core.Utility;
using Code.EntityScripts.Components;
using Code.EntityScripts.Interface;
using Member.YTH.Code.Item;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.EntityScripts.BaseClass {
    public abstract class Entity : MonoBehaviour {
        private Dictionary<Type, IEntityModule> _moduleDict;
        protected EntityHealth Health; // Entity와 Health는 강한 결합

        [field: SerializeField] public EntityDataSO Data { get; private set; }

        public bool IsDead { get; protected set; }

        public void DropItem() {
            if (!Data) {
                Logging.LogError("데이터가 없어요");
                return;
            }

            if (Data.DropTable == null) return;

            foreach (var dropData in Data.DropTable) {
                for (int i = 0; i < dropData.Count; i++) {
                    if (Random.value > dropData.DropRate) continue;
                    var item = PoolManager.Instance.Factory<ItemObject>().Pop();
                    item.SetItemData(dropData.Item);
                    item.transform.position = transform.position;
                    item.AddForce(Random.insideUnitCircle * 2f, ForceMode2D.Impulse);
                }
            }
        }

        public virtual void PushToPool(){}
        
        public virtual void ResetEntity(){}

        protected virtual void Awake() {
            _moduleDict = GetComponentsInChildren<IEntityModule>(true)
                .ToDictionary(compo => compo.GetType());

            InitializeComponents();
            Health = GetModule<EntityHealth>();
        }

        protected virtual void InitializeComponents() {
            foreach (var compo in _moduleDict.Values) {
                compo.Initialize(this);
            }
        }

        public T GetModule<T>() {
            if (_moduleDict.TryGetValue(typeof(T), out var module) && module is T compo) {
                return compo;
            }

            IEntityModule findComponent = _moduleDict.Values.FirstOrDefault(m => m is T);
            if (findComponent is T findCompo)
                return findCompo;

            return default;
        }
    }
}