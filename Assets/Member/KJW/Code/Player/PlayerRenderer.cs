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
        
        private Arm _arm;

        private void Awake()
        {
            _arm = transform.root.GetComponentInChildren<Arm>();
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