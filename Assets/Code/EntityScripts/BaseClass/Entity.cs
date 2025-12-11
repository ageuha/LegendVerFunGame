using System;
using System.Collections.Generic;
using System.Linq;
using Code.EntityScripts.Components;
using Code.EntityScripts.Interface;
using UnityEngine;

namespace Code.EntityScripts.BaseClass {
    public abstract class Entity : MonoBehaviour {
        private Dictionary<Type, IEntityModule> _moduleDict;
        protected EntityHealth Health; // Entity와 Health는 강한 결합

        [field: SerializeField] public EntityDataSO Data { get; private set; }

        public bool IsDead { get; protected set; }

        public abstract void DropItem();

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