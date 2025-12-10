using Code.AnimatorSystem;
using Code.Core.GlobalSO;
using Code.EntityScripts.BaseClass;
using Code.EntityScripts.Interface;
using UnityEngine;

namespace Code.EntityScripts.Components {
    public class EntityRenderer : AnimatorCompo, IShaderController, IEntityModule {
        [SerializeField] private SpriteRenderer sr;
        private Material _material;

        protected override void Reset() {
            base.Reset();
            sr ??= GetComponent<SpriteRenderer>();
        }

        private void Awake() {
            _material = sr.material;
        }

        public void SetShaderValue(HashSO shaderID, float value) {
            _material.SetFloat(shaderID, value);
        }

        public void Initialize(Entity owner) {
            
        }
    }
}