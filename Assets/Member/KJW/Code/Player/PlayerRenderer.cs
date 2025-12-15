using System;
using Code.AnimatorSystem;
using Code.Core.GlobalSO;
using Member.KJW.Code.CombatSystem;
using UnityEngine;

namespace Member.KJW.Code.Player
{
    public class PlayerRenderer : AnimatorCompo
    {
        [SerializeField] private HashSO endAttackHash;
        [SerializeField] private HashSO shaderHash;
        private SpriteRenderer _spriteRenderersr;
        private Material _material;
        private Arm _arm;

        private void Awake()
        {
            _spriteRenderersr = GetComponent<SpriteRenderer>();
            _arm = transform.root.GetComponentInChildren<Arm>(true);
            _material = _spriteRenderersr.material;
        }
        
        public void SetShaderValue(float value) {
            _material.SetFloat(shaderHash, value);
        }

        public void GetShaderValue()
        {
            _material.GetFloat(shaderHash);
        }

        public void SetFlip(float xVelocity)
        {
            transform.localRotation = Quaternion.Euler(0f, xVelocity > 0 ? 180f : 0f, 0f);
        }

        public void SetFalseArm()
        {
            _arm.gameObject.SetActive(false);
            SetValue(endAttackHash);
        }
    }
}