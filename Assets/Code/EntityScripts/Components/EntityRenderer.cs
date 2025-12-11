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

        public void SetFlip(float xVelocity) {
            transform.localRotation = Quaternion.Euler(0f, xVelocity > 0 ? 180f : 0f, 0f);
        }

        public void Initialize(Entity owner) {
        }
    }
}